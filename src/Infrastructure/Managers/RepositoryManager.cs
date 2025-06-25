using Domain.Managers;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class RepositoryManager : IRepositoryManager
    {
        private bool _disposed = false;

        private readonly Lazy<IQuestionRepository> _QuestionRepository;
        private readonly Lazy<ILanguageRepository> _LanguageRepository;
        private readonly Lazy<ITagRepository> _TagRepository;
        private readonly Lazy<ISourceRepository> _SourceRepository;
        private readonly Lazy<ICategoryRepository> _CategoryRepository;
        private readonly Lazy<IApplicationLogRepository> _ApplicationLogRepository;


        public RepositoryManager(IConfiguration configuration)
        {
            _QuestionRepository = new Lazy<IQuestionRepository>(() => new QuestionRepository(configuration.GetConnectionString("Examiner")));
            _LanguageRepository = new Lazy<ILanguageRepository>(() => new LanguageRepository(configuration.GetConnectionString("Examiner")));
            _TagRepository = new Lazy<ITagRepository>(() => new TagRepository(configuration.GetConnectionString("Examiner")));
            _SourceRepository = new Lazy<ISourceRepository>(() => new SourceRepository(configuration.GetConnectionString("Examiner")));
            _CategoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(configuration.GetConnectionString("Examiner")));
        }

        public IQuestionRepository QuestionRepository => _QuestionRepository.Value;
        public ILanguageRepository LanguageRepository => _LanguageRepository.Value;
        public ITagRepository TagRepository => _TagRepository.Value;
        public ISourceRepository SourceRepository => _SourceRepository.Value;
        public ICategoryRepository CategoryRepository => _CategoryRepository.Value;
        public IApplicationLogRepository ApplicationLogRepository => _ApplicationLogRepository.Value;


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
