using Domain.Repositories;

namespace Domain.Managers
{
    public interface IRepositoryManager
    {
        public IQuestionRepository QuestionRepository { get;}
    }
}
