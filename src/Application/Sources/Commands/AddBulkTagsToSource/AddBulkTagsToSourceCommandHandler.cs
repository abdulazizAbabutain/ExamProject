using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Commands.AddBulkTagsToSource
{
    public class AddBulkTagsToSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddBulkTagsToSourceCommand, Result<PartialsSuccessResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result<PartialsSuccessResult>> Handle(AddBulkTagsToSourceCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.SourceService.AddBulkTag(request.SourceId,request.TagIds);
        }
    }
}
