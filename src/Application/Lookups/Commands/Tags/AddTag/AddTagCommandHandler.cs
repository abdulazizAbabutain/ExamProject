using Application.Commons.Managers;
using MediatR;

namespace Application.Lookups.Commands.Tags.AddTag
{
    public class AddTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddTagCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.LookupService.AddTag(request.Name,request.ColorCode);
            _serviceManager.Dispose();
        }
    }
}
