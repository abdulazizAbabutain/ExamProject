using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.UnArchiveTag
{
    public class UnArchiveTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<UnarchiveTagCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(UnarchiveTagCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.TagService.UnArchiveTag(request.Id);
        }
    }
}
