using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.ArchiveTag
{
    public class ArchiveTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<ArchiveTagCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(ArchiveTagCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.TagService.ArchiveTag(request.Id);
        }
    }
}
