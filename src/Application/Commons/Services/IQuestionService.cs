using Domain.Entities.Examiner;

namespace Application.Commons.Services
{
    public interface IQuestionService : IDisposable
    {
        Question InsertQuestion(Question question);
        IEnumerable<Question> GetQuestions(int pageNumber, int pageSize);
        Question GetQuestion(Guid id);
        int Count();
        void DeleteQuestion(Guid id);
        void UpdateQuestion(Question question);
        void DeleteAllQuestions();
    }
}
