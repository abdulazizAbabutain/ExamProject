using Application.Commons.Services;
using Domain.Entities.Examiner;
using Domain.Repositories;

namespace Infrastructure.Services;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    private bool _disposed = false;
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

    public void UpdateQuestion(Question question)
    {
        _questionRepository.Update(question);
    }

    public void DeleteQuestion(Guid id)
    {
        _questionRepository.DeleteById(id);
    }

    public void DeleteAllQuestions()
    {
       _questionRepository.DeleteAll();
    }

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
              
            }
            _disposed = true;
        }
    }
    #endregion
}
