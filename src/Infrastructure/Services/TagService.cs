using Application.Commons.Extensions;
using Application.Commons.Managers;
using Application.Commons.Models.Icons;
using Application.Commons.Models.Results;
using Application.Commons.Services;
using Domain.Auditing;
using Domain.Constants;
using Domain.Entities.EntityLookup;
using Domain.Entities.Sources;
using Domain.Enums;
using Domain.Extentions;
using Domain.Managers;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Services
{
    public class TagService(IRepositoryManager repositoryManager, IAuditManager auditManager, IStringLocalizer<TagService> localizer) : ITagService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IAuditManager _auditManager = auditManager;
        private readonly IStringLocalizer _localizer = localizer;



        #region Tag services
        public Result<Tag> AddTag(string name, string? backgroundColorCode = null, string testColorCode = null, IconCommand icon = null)
        {
            string normalizedName = TextNormalizer.Normalize(name);

            var duplicates = _repositoryManager.TagRepository.GetDuplication(normalizedName);

            if (backgroundColorCode.IsNull())
                backgroundColorCode = ColorExtension.GenerateRandomHexColor();

            DuplicationReviewMetadata? duplicationReview = null;

            if (duplicates.Any())
                duplicationReview = DuplicationReviewMetadata.CreateDetected(duplicates.ToList());


            Tag tag;

            if (icon.IsNotNull())
                tag = new Tag(name, backgroundColorCode!, duplicationReview, testColorCode, icon.IconUrl!, icon.IconColor!);
            else
                tag = new Tag(name, backgroundColorCode!, duplicationReview, testColorCode);



            _repositoryManager.TagRepository.Insert(tag);
            _auditManager.AuditTrailService.AddNewEntity(EntityName.Tag, tag.Id, ActionBy.User, tag, tag.VersionNumber);
            return Result<Tag>.Success(tag);
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _repositoryManager.TagRepository.GetAll();
        }

        /// <summary>
        /// the service will check if there different tag with same name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">the new Name</param>
        /// <param name="backgroundColorCode">the color in hex code</param>
        public Result UpdateTag(Guid id, string name, string backgroundColorCode, string textColorCode)
         {

            var tag = _repositoryManager.TagRepository.GetById(id);

            if (tag.IsNull())
                return Result.NotFoundFailure(nameof(id), _localizer[ErrorMessage.NOT_FOUND_ENTITY, nameof(EntityName.Tag), id]);

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            if (backgroundColorCode.IsNull())
                backgroundColorCode = ColorExtension.GenerateRandomHexColor();


            string normalizedName = TextNormalizer.Normalize(name);
            var duplicates = _repositoryManager.TagRepository
                .GetDuplication(normalizedName, excludeId: id);

            if (duplicates.Any())
                tag.MarkAsDuplicate(duplicates.ToList());

            else if (tag.DuplicationReview.IsNotNull())
                tag.ResolveDuplicateFromUpdate();


            tag.UpdateTag(name, backgroundColorCode, textColorCode);
            _repositoryManager.TagRepository.Update(tag);

            _auditManager.AuditTrailService.UpdateEntity(EntityName.Tag, id, ActionType.Modified, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }
        /// <summary>
        /// delete a tag and all related entities with a tag.
        /// </summary>
        /// <param name="id"></param>
        public Result DeleteTag(Guid id)
        {
            if (_repositoryManager.TagRepository.IsNotExist(id))
                return Result.NotFoundFailure(nameof(id), _localizer[ErrorMessage.TAG_WITH_SAME_NAME_EXISTS, nameof(EntityName.Tag), id]);

            var sources = _repositoryManager.SourceRepository.GetCollection().Find(s => s.Tags.Contains(id)).ToList();
            if (sources.Any() && sources.IsNotNull())
            {
                var auditTrailSources = new List<(Guid EntityId, ActionType ActionType, ActionBy ActionBy, Source OldEntity, Source NewEntity, int Version, string? Comment)>();
                foreach (var source in sources)
                {
                    var sourceClone = FastDeepCloner.DeepCloner.Clone(source);
                    source.RemoveTag(id);
                    auditTrailSources.Add((
                        entityId: source.Id,
                        actionType: ActionType.Modified,
                        actionBy: ActionBy.System,
                        oldEntity: sourceClone,
                        newEntity: source,
                        version: source.VersionNumber,
                        comment: $"removing all related with tag id {id}"
                    ));
                }
                _repositoryManager.SourceRepository.Update(sources);
                _auditManager.AuditTrailService.UpdateEntitiesBulk(EntityName.Source, auditTrailSources);
            }

            //var questions = _repositoryManager.QuestionRepository.GetCollection().Find(s => s.Tags.Contains(id)).ToList();

            //if (questions.Any() && questions.IsNotNull())
            //{
            //    foreach (var question in questions)
            //    {
            //        question.RemoveTag(id);
            //    }
            //    _repositoryManager.SourceRepository.Update(sources);
            //}

            var tag = _repositoryManager.TagRepository.GetById(id);
            _repositoryManager.TagRepository.DeleteById(id);
            _auditManager.AuditTrailService.DeleteEntity(EntityName.Tag, id, ActionType.Deleted, ActionBy.User, tag, tag.VersionNumber, "Delete teg");
            return Result.Success();
        }
        public Result ArchiveTag(Guid id)
        {
            var tag = _repositoryManager.TagRepository.GetById(id);
            if (tag.IsNull())
                return Result.NotFoundFailure(nameof(id), _localizer[ErrorMessage.NOT_FOUND_ENTITY, nameof(EntityName.Tag), id]);

            if (tag.IsArchived)
                return Result.ConflictFailure(nameof(id), _localizer[ErrorMessage.ENTITY_IS_ARCHIVED, id]);

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            tag.ArchiveTag();
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntityName.Tag, id, ActionType.Archived, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }


        public Result ArchiveAllTag()
        {
            var tags = _repositoryManager.TagRepository.GetCollection()
                .Find(t => !t.IsArchived)
                .ToList();

            if (!tags.Any())
                return Result.UnprocessableEntityFailure("", "There is no tag to be archive");

            var updatedTags = new List<Tag>();
            var audits = new List<(Guid Id, Tag Old, Tag New, int Version)>();

            foreach (var tag in tags)
            {
                var tagClone = CloneExtension.DeepClone(tag);
                tag.ArchiveTag();
                updatedTags.Add(tag);
                audits.Add((tag.Id, tagClone, tag, tag.VersionNumber));
            }

            _repositoryManager.TagRepository.Update(updatedTags);

            foreach (var (id, oldTag, newTag, version) in audits)
            {
                _auditManager.AuditTrailService.UpdateEntity(
                    EntityName.Tag,
                    id,
                    ActionType.UnArchived,
                    ActionBy.User,
                    oldTag,
                    newTag,
                    version
                );
            }
            return Result.NoContentSuccess();
        }


        public Result UnArchiveTag(Guid id)
        {
            var tag = _repositoryManager.TagRepository.GetById(id);
            if (tag.IsNull())
                return Result.NotFoundFailure(nameof(id), $"Tag with id {id} were not found");

            if (!tag.IsArchived)
                return Result.ConflictFailure(nameof(id), $"Tag with id {id} is already unarchived");

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            tag.UnArchiveTag();
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntityName.Tag, id, ActionType.UnArchived, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }


        public Result ResolveDuplication(Guid id, ReviewDuplicationStatus reviewStatus)
        {
            var tag = _repositoryManager.TagRepository.GetById(id);
            if (tag.IsNull())
                return Result.NotFoundFailure(nameof(id), _localizer[ErrorMessage.NOT_FOUND_ENTITY,id]);

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            tag.ResolveDuplicate(reviewStatus);
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntityName.Tag, id, ActionType.DuplicationReviewed, ActionBy.User, tagClone, tag, tag.VersionNumber);


            return Result.NoContentSuccess();
        }
        #endregion


    }
}
