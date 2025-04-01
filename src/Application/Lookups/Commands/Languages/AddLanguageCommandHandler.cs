using Application.Commons.Managers;
using Domain.Entities.EntityLookup;
using MediatR;

namespace Application.Lookups.Commands.Languages
{
    public class AddLanguageCommandHandler(IServiceManager serviceManager) : IRequestHandler<AddLanguageCommand>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task Handle(AddLanguageCommand request, CancellationToken cancellationToken)
        {
            var lang = new Language(request.Code, request.DisplayName);
            _serviceManager.LookupService.AddLanguage(lang);
            _serviceManager.Dispose();
        }
    }
}
