using Domain.Repositories;

namespace Domain.Managers
{
    public interface IRepositoryManager : IDisposable
    {
        public IQuestionRepository QuestionRepository { get;}
        public ILanguageRepository LanguageRepository { get;}
        public ITagRepository TagRepository { get;}
        public ISourceRepository SourceRepository { get;}
        public ICategoryRepository CategoryRepository { get; }
        public IApplicationLogRepository ApplicationLogRepository {  get; }
    }
}
