using Application.Commons.Extensions;
using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.Services;
using Domain.Entities.EntityLookup;
using Domain.Entities.Sources;
using Domain.Enums;
using Domain.Extentions;
using Domain.Managers;
using System.Security.Principal;

namespace Infrastructure.Services
{
    public class TagService(IRepositoryManager repositoryManager, IAuditManager auditManager) : ITagService
    {
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IAuditManager _auditManager = auditManager;



        #region Tag services
        public Result<Tag> AddTag(string name, string? colorCode = null)
        {
            if (_repositoryManager.TagRepository.IsExist(name))
                return Result<Tag>.ConflictFailure($"Tag with name {name} already exists.");

            if (colorCode.IsNull())
                colorCode = ColorExtension.GenerateRandomHexColor();
            var tag = new Tag(name, colorCode);
            _repositoryManager.TagRepository.Insert(tag);
            _auditManager.AuditTrailService.AddNewEntity(EntitiesName.Tag, tag.Id, ActionBy.User, tag, tag.VersionNumber);
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
        /// <param name="colorCode">the color in hex code</param>
        public Result UpdateTag(Guid id, string name, string? colorCode = null)
        {
            if (_repositoryManager.TagRepository.IsExist(name,id))
                return Result.ConflictFailure($"Tag with name {name} is already exists");

            var tag = _repositoryManager.TagRepository.GetById(id);

            if (tag.IsNull())
                return Result.NotFoundFailure($"tag with id {id} was not found") ;

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            if (colorCode.IsNull())
                colorCode = ColorExtension.GenerateRandomHexColor();

            tag.UpdateTag(name, colorCode);
            _repositoryManager.TagRepository.Update(tag);

            _auditManager.AuditTrailService.UpdateEntity(EntitiesName.Tag, id, ActionType.Modified, ActionBy.User,tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }
        /// <summary>
        /// delete a tag and all related entities with a tag.
        /// </summary>
        /// <param name="id"></param>
        public Result DeleteTag(Guid id)
        {
            if (_repositoryManager.TagRepository.IsNotExist(id))
                return Result.NotFoundFailure($"tag with id {id} was not found");


            //TODO: link to tags after compate it.
            //var sources = _repositoryManager.SourceRepository.GetCollection().Find(s => s.Tags.Contains(id)).ToList();
            //if (sources.Any() && sources.IsNotNull())
            //{
            //    var auditTrailSources = new List<(Guid EntityId, ActionType ActionType, ActionBy ActionBy, Source OldEntity, Source NewEntity, int Version, string? Comment)>();
            //    foreach (var source in sources)
            //    {
            //        var sourceClone = FastDeepCloner.DeepCloner.Clone(source);
            //        source.RemoveTag(id);
            //        auditTrailSources.Add((
            //            entityId: Guid.NewGuid(),
            //            actionType: ActionType.Modified,
            //            actionBy: ActionBy.System,
            //            oldEntity: sourceClone,
            //            newEntity: source,
            //            version: source.VersionNumber,
            //            comment: "update the source to remove tag"
            //        ));
            //    }
            //    _repositoryManager.SourceRepository.Update(sources);
            //    _auditManager.AuditTrailService.UpdateEntitiesBulk(EntitiesName.Source, auditTrailSources);
            //}

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
            _auditManager.AuditTrailService.DeleteEntity(EntitiesName.Tag,id,ActionType.Deleted,ActionBy.User, tag, tag.VersionNumber, "Delete teg");
            return Result.Success();
        }
        public Result ArchiveTag(Guid id)
        {
            var tag = _repositoryManager.TagRepository.GetById(id);
            if (tag.IsNull())
                return Result.NotFoundFailure($"Tag with id {id} were not found");

            if(tag.IsArchived)
                return Result.ConflictFailure($"Tag with id {id} is already archived");

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            tag.ArchiveTag();
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntitiesName.Tag, id, ActionType.Archived, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }


        public Result ArchiveAllTag()
        {
            var tags = _repositoryManager.TagRepository.GetCollection()
                .Find(t => !t.IsArchived) 
                .ToList();

            if (!tags.Any())
                return Result.UnprocessableEntityFailure("There is no tag to be archive");

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
                    EntitiesName.Tag,
                    id,
                    ActionType.Archived,
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
                return Result.NotFoundFailure($"Tag with id {id} were not found");

            if (!tag.IsArchived)
                return Result.ConflictFailure($"Tag with id {id} is already unarchived");

            var tagClone = FastDeepCloner.DeepCloner.Clone(tag);

            tag.UnArchiveTag();
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntitiesName.Tag, id, ActionType.Archived, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }
        #endregion


    }
}
