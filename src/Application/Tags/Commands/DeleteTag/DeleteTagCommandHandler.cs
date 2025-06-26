using Application.Commons.Managers;
using MediatR;

namespace Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<DeleteTagCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            _serviceManager.TagService.DeleteTag(request.Id);
        }
    }
}
