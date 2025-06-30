using Application.Commons.Services;

namespace Application.Commons.Managers;

public interface IServiceManager : IDisposable
{
    IQuestionService QuestionService { get; }
    ILookupService LookupService { get; }
    ITagService TagService { get; }
    ISourceService SourceService { get; }

}
