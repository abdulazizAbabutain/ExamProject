using Application.Commons.Extensions;
using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.Services;
using Domain.Entities.EntityLookup;
using Domain.Enums;
using Domain.Extentions;
using Domain.Managers;

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
            _auditManager.AuditTrailService.AddNewEntity(EntitiesName.Tag, tag.Id, ActionBy.User, tag.VersionNumber);
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
            if (_repositoryManager.TagRepository.IsExist(name))
                return Result.ConflictFailure($"Tag with name {name} is already exists");

            var tag = _repositoryManager.TagRepository.GetById(id);

            if (tag.IsNull())
                return Result.NotFoundFailure($"tag with id {id} was not found") ;

            var tagClone = CloneExtension.DeepClone(tag);

            if (colorCode.IsNull())
                colorCode = ColorExtension.GenerateRandomHexColor();

            tag.UpdateTag(name, colorCode);

            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntitiesName.Tag, id, ActionType.Modified, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.NoContentSuccess();
        }
        /// <summary>
        /// delete a tag and all related entities with a tag.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTag(Guid id)
        {
            //TODO: add exception handling
            if (_repositoryManager.TagRepository.IsNotExist(id))
                return;

            var sources = _repositoryManager.SourceRepository.GetCollection().Find(s => s.Tags.Contains(id)).ToList();
            if (sources.Any() && sources.IsNotNull())
            {
                foreach (var source in sources)
                {
                    source.RemoveTag(id);
                }
                _repositoryManager.SourceRepository.Update(sources);
            }

            var questions = _repositoryManager.QuestionRepository.GetCollection().Find(s => s.Tags.Contains(id)).ToList();

            if (questions.Any() && questions.IsNotNull())
            {
                foreach (var question in questions)
                {
                    question.RemoveTag(id);
                }
                _repositoryManager.SourceRepository.Update(sources);
            }

            _repositoryManager.TagRepository.DeleteById(id);
        }
        public Result ArchiveTag(Guid id)
        {
            var tag = _repositoryManager.TagRepository.GetById(id);
            if (tag.IsNull())
                return Result.NotFoundFailure($"Tag with id {id} were not found");

            if(tag.IsArchived)
                return Result.ConflictFailure($"Tag with id {id} is already archived");

            var tagClone = CloneExtension.DeepClone(tag);

            tag.ArchiveTag();
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntitiesName.Tag, id, ActionType.Archived, ActionBy.User, tagClone, tag, tag.VersionNumber);
            return Result.Success();
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


        public void UnArchiveTag(Guid id)
        {
            var tag = _repositoryManager.TagRepository.GetById(id);
            var tagClone = CloneExtension.DeepClone(tag);

            if (tag.IsNull())
                return;

            tag.UnArchiveTag();
            _repositoryManager.TagRepository.Update(tag);
            _auditManager.AuditTrailService.UpdateEntity(EntitiesName.Tag, id, ActionType.UnArchived, ActionBy.User, tagClone, tag, tag.VersionNumber);

        }
        #endregion


    }
}
