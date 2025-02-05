using Domain.Entities.Examiner;
using Domain.Entities.History;

namespace Application.Commons.Services
{
    public interface IQuestionService
    {
        Question InsertQuestion(Question question);
        IEnumerable<Question> GetQuestions(int pageNumber, int pageSize);
        Question GetQuestion(Guid id);
        int Count();
        IEnumerable<QuestionHistory> GetHistories(Guid questionId, int pageNumber, int pageSize);
    }
}
