using Application.Commons.Managers;
using Application.Commons.Services;
using Domain.Managers;
using Infrastructure.Services;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Managers;

public class ServiceManager(IRepositoryManager repositoryManager, IAuditManager auditManager, IMapper mapper, IServiceProvider serviceProvider) : IServiceManager
{
    private bool _disposed = false;

   
    private readonly Lazy<IQuestionService> _QuestionService = 
        new(() => new QuestionService(repositoryManager.QuestionRepository));
    private readonly Lazy<ILookupService> _LookupService = 
        new(() => new LookupService(repositoryManager, auditManager));
    private readonly Lazy<ITagService> _TagService = 
        new(() => new TagService(repositoryManager, auditManager, serviceProvider.GetRequiredService<IStringLocalizer<TagService>>()));
    private readonly Lazy<ISourceService> _sourceService = 
        new(() => new SourceService(repositoryManager, auditManager, mapper));
    private readonly Lazy<ICategoryService> _categoryService = 
        new(() => new CategoryService(repositoryManager, auditManager, mapper));

    public IQuestionService QuestionService => _QuestionService.Value;
    public ILookupService LookupService => _LookupService.Value;
    public ITagService TagService => _TagService.Value;
    public ISourceService SourceService => _sourceService.Value;
    public ICategoryService CategoryService => _categoryService.Value;


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
