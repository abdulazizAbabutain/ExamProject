using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Commands.RemoveBulkTagFromSource
{
    internal class RemoveBulkTagFromSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<RemoveBulkTagFromSourceCommand, Result<PartialsSuccessResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result<PartialsSuccessResult>> Handle(RemoveBulkTagFromSourceCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.SourceService.RemoveBulkTags(request.SourceId,request.TagIds);
        }
    }
}
