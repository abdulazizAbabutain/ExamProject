using Application.Commons.Managers;
using MediatR;

namespace Application.Lookups.Commands.Sources.UpdateSource;

public class UpdateSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<UpdateSourceCommand>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task Handle(UpdateSourceCommand request, CancellationToken cancellationToken)
    {
        var source = _serviceManager.LookupService.GetSource(request.Id);
        source.UpdateSource(request.Type,request.Title,request.Description,request.Tags);
        _serviceManager.LookupService.UpdateSource(source);
    }
}
