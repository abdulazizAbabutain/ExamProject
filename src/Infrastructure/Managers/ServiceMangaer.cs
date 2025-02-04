using Application.Commons.Managers;
using Application.Commons.Services;
using Domain.Managers;
using Infrastructure.Services;

namespace Infrastructure.Managers;

public sealed class ServiceMangaer : IServiceManager
{
    private readonly Lazy<IQuestionService> _QuestionService;
    public ServiceMangaer(IRepositoryManager repositoryManager)
    {
        _QuestionService = new Lazy<IQuestionService>(() => new QuestionService(repositoryManager.QuestionRepository));
    }

    public IQuestionService QuestionService => _QuestionService.Value;
}
