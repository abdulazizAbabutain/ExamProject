using Application.Commons.Services;

namespace Application.Commons.Managers;

public interface IServiceManager
{
    IQuestionService QuestionService { get; }
}
