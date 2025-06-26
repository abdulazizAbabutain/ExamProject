using Application.Commons.Managers;
using MediatR;

namespace Application.Tags.Commands.UnArchiveTag
{
    public class UnArchiveTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<UnarchiveTagCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(UnarchiveTagCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.TagService.UnArchiveTag(request.Id);
        }
    }
}
