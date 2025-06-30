using Domain.Managers;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class RepositoryManager(IConfiguration configuration) : IRepositoryManager
    {
        private bool _disposed = false;

        private readonly Lazy<IQuestionRepository> _QuestionRepository = new(() => new QuestionRepository(configuration.GetConnectionString("Examiner")));
        private readonly Lazy<ILanguageRepository> _LanguageRepository = new(() => new LanguageRepository(configuration.GetConnectionString("Examiner")));
        private readonly Lazy<ITagRepository> _TagRepository = new(() => new TagRepository(configuration.GetConnectionString("Examiner")));
        private readonly Lazy<ISourceRepository> _SourceRepository = new(() => new SourceRepository(configuration.GetConnectionString("Examiner")));
        private readonly Lazy<ICategoryRepository> _CategoryRepository = new(() => new CategoryRepository(configuration.GetConnectionString("Examiner")));
        private readonly Lazy<IReferenceRepository> _ReferenceRepository = new(() => new ReferenceRepository(configuration.GetConnectionString("Examiner")));

        public IQuestionRepository QuestionRepository => _QuestionRepository.Value;
        public ILanguageRepository LanguageRepository => _LanguageRepository.Value;
        public ITagRepository TagRepository => _TagRepository.Value;
        public ISourceRepository SourceRepository => _SourceRepository.Value;
        public ICategoryRepository CategoryRepository => _CategoryRepository.Value;
        public IReferenceRepository ReferenceRepository => _ReferenceRepository.Value;

        public void Dispose()
        {
            Dispose(true);
            // This object will not be finalized, so call:
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if(_QuestionRepository.IsValueCreated)
                        QuestionRepository.Dispose();
                    if (_LanguageRepository.IsValueCreated)
                        LanguageRepository.Dispose();
                    if(_TagRepository.IsValueCreated)
                        TagRepository.Dispose();
                    if (_SourceRepository.IsValueCreated)
                        SourceRepository.Dispose();
                }
                _disposed = true;
            }
        }

    }
}
