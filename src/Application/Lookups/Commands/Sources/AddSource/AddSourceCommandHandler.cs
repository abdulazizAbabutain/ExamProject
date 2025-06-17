using Application.Commons.Managers;
using Domain.Entities.Sources;
using MediatR;

namespace Application.Lookups.Commands.Sources.AddSource
{
    public class AddSourceCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddSourceCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(AddSourceCommand request, CancellationToken cancellationToken)
        {
            var source = new Source(request.Type, request.Title, request.Description, request.Tags);
            _serviceManager.LookupService.AddSource(source);
        }
    }
}
