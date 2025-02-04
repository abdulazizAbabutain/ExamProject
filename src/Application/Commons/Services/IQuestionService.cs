using Domain.Entities.Examiner;

namespace Application.Commons.Services
{
    public interface IQuestionService
    {
        Question InsertQuestion(Question question);

        IEnumerable<Question> GetQuestions();
        Question GetQuestion(Guid id);
    }
}
