using Application.Commons.Services;
using Domain.Entities.EntityLookup;
using Domain.Entities.Sources;
using Domain.Extentions;
using Domain.Managers;

namespace Infrastructure.Services
{
    public class LookupService : ILookupService
    {
        private bool _disposed = false;
        private readonly IRepositoryManager _repositoryManager;

        public LookupService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        #region Language services
        public void AddLanguage(Language language)
        {
            // TODO: Adding business expextion if lancode is exists will return a result that lang can not be added.

            if (_repositoryManager.LanguageRepository.IsLangCodeExists(language.Code))
                return;

            _repositoryManager.LanguageRepository.Insert(language);
        }

        public IEnumerable<Language> GetLanguages()
        {
            return _repositoryManager.LanguageRepository.GetAll();
        }
        public Guid GetLanguageReference(string code)
        {
            return _repositoryManager.LanguageRepository.GetLanguageReference(code);
        }
        #endregion

        #region Tag services

        public void AddTag(string name, string? colorCode = null)
        {
            if (_repositoryManager.TagRepository.IsExist(name))
                return;

            if (colorCode.IsNull())
                colorCode = ColorExtension.GenerateRandomHexColor();

            _repositoryManager.TagRepository.Insert(new Tag(name, colorCode));
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _repositoryManager.TagRepository.GetAll();
        }
        /// <summary>
        /// get all tags that refrence, if the data is not found it will be created.
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetTagsReference(IEnumerable<string> tags)
        {
            var existsTags = _repositoryManager.TagRepository.GetAllAvailableTags(tags);
            var newTags = tags.Except(existsTags);
            if (newTags.Any())
                _repositoryManager.TagRepository.Insert(newTags.Select(t => new Tag(t, "#FF5733")).ToList());

            return _repositoryManager.TagRepository.GetTagsReference(tags);
        }
        public void UpdateTag(Guid id, string name, string? colorCode = null)
        {
            if (_repositoryManager.TagRepository.IsExist(name))
                return;

            var tag = _repositoryManager.TagRepository.GetById(id);

            if (tag.IsNull())
                return;
            if (colorCode.IsNull())
                colorCode = ColorExtension.GenerateRandomHexColor();

            tag.UpdateTag(name, colorCode);

            _repositoryManager.TagRepository.Update(tag);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTag(Guid id)
        {
            //TODO: add exception handling
            if (_repositoryManager.TagRepository.IsNotExist(id))
                return;
            
            var sources = _repositoryManager.SourceRepository.GetCollection().Find(s => s.Tags.Contains(id)).ToList();
            if(sources.Any() && sources.IsNotNull())
            {
                foreach(var source in sources)
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
        public void ArchiveTag(Guid id)
        {
            _repositoryManager.TagRepository.ArchiveTag(id);
        }

        public void UnArchiveTag(Guid id)
        {
            _repositoryManager.TagRepository.UnArchiveTag(id);
        }

        #endregion

        #region Source services
        public void AddSource(Source source)
        {
            _repositoryManager.SourceRepository.Insert(source);
        }

        public void UpdateSource(Source source)
        {
            _repositoryManager.SourceRepository.Update(source);
        }

        public Source GetSource(Guid id)
        {
            return _repositoryManager.SourceRepository.GetById(id);
        }
        #endregion

        #region category services 

        public void AddCategory(string name, Guid? parentId)
        {
            if (parentId.HasValue)
            {
                var category = _repositoryManager.CategoryRepository.GetById(parentId.Value);
                if (category.IsNull())
                    return;

                _repositoryManager.CategoryRepository.Insert(new Category(name, parentId.Value, category.Level));
            }
            else
            {
                _repositoryManager.CategoryRepository.Insert(new Category(name));
            }
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositoryManager.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
