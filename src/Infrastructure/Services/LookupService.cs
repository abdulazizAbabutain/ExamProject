using Application.Commons.Managers;
using Application.Commons.Services;
using Domain.Entities.EntityLookup;
using Domain.Entities.Sources;
using Domain.Extentions;
using Domain.Managers;

namespace Infrastructure.Services
{
    public class LookupService(IRepositoryManager repositoryManager, IAuditManager auditManager) : ILookupService
    {
        private bool _disposed = false;
        private readonly IRepositoryManager _repositoryManager = repositoryManager;
        private readonly IAuditManager _auditManager = auditManager;

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
