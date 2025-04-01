using Application.Commons.Managers;
using Application.Commons.Services;
using Domain.Extentions;
using Domain.Managers;
using Infrastructure.Repositories;
using Infrastructure.Services;

namespace Infrastructure.Managers;

public class ServiceMangaer : IServiceManager
{
    private bool _disposed = false;

    private readonly Lazy<IQuestionService> _QuestionService;
    private readonly Lazy<ILookupService> _LookupService;
    public ServiceMangaer(IRepositoryManager repositoryManager)
    {
        _QuestionService = new Lazy<IQuestionService>(() => new QuestionService(repositoryManager.QuestionRepository));
        _LookupService= new Lazy<ILookupService>(() => new LookupService(repositoryManager));
    }

    public IQuestionService QuestionService => _QuestionService.Value;
    public ILookupService LookupService => _LookupService.Value;

    public void Dispose()
    {
        Dispose(true);
        // This object will not be finalized, so call:
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                if(_QuestionService.IsValueCreated)
                    QuestionService.Dispose();
                if(_LookupService.IsValueCreated)
                    LookupService.Dispose();
            }

            _disposed = true;
        }
    }
}
