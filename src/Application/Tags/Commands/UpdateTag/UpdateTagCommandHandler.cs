using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler(IServiceManager serviceManager) : IRequestHandler<UpdateTagCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.TagService.UpdateTag(request.Id, request.Name, request.BackgroundColorCode,request.TextColorCode);
        }
    }
}
