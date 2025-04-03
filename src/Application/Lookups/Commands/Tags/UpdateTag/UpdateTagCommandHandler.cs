using Application.Commons.Managers;
using MediatR;

namespace Application.Lookups.Commands.Tags.UpdateTag
{
    public class UpdateTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<UpdateTagCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.LookupService.UpdateTag(request.Id, request.Name, request.ColorHexCode);
        }
    }
}
