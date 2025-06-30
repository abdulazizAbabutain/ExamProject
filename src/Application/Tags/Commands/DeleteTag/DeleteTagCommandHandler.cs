using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<DeleteTagCommand,Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.TagService.DeleteTag(request.Id);
        }
    }
}
