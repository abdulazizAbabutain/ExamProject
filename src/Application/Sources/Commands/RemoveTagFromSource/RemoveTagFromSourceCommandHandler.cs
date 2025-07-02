using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Sources.Commands.RemoveTagFromSource
{
    public class RemoveTagFromSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<RemoveTagFromSourceCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(RemoveTagFromSourceCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.SourceService.RemoveTag(request.SourceId,request.TagId);
        }
    }
}
