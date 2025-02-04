using Application.Commons.Services;
using Domain.Entities.Examiner;
using Domain.Repositories;

namespace Infrastructure.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(IQuestionRepository));

    public IEnumerable<Question> GetQuestions()
    {
        return _questionRepository.GetAll();
    }

    public Question InsertQuestion(Question question)
    {
        _questionRepository.Insert(question);
        return question;
    }

    public Question GetQuestion(Guid id)
    {
        return _questionRepository.GetById(id);
    }
}
