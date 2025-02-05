using Application.Commons.Services;
using Domain.Entities.Examiner;
using Domain.Entities.History;
using Domain.Repositories;

namespace Infrastructure.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(IQuestionRepository));

    public IEnumerable<Question> GetQuestions(int pageNumber , int pageSize)
    {
        return _questionRepository.GetAll(pageNumber,pageSize);
    }

    public int Count()
    {
        return _questionRepository.Count();
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

    public IEnumerable<QuestionHistory> GetHistories(Guid questionId, int pageNumber, int pageSize)
    {
        var histories = _questionRepository.GetById(questionId).Histories;
        if(histories == null)
            return new List<QuestionHistory>();

        return histories.Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
    }
}
