using Application.Commons.Managers;
using MediatR;

namespace Application.Tags.Commands.ArchiveTag
{
    public class ArchiveTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<ArchiveTagCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(ArchiveTagCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.LookupService.ArchiveTag(request.Id);
        }
    }
}
