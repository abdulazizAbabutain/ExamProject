using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Commands.AddTagToSource;

public class AddTagsToSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddTagsToSourceCommand, Result>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<Result> Handle(AddTagsToSourceCommand request, CancellationToken cancellationToken)
    {
        return _serviceManager.SourceService.AddTag(request.SourceId,request.TagId);
    }
}
