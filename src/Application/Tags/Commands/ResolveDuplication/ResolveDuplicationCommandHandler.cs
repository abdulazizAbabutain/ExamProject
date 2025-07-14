using Application.Commons.Managers;
using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Commands.ResolveDuplication
{
    public class ResolveDuplicationCommandHandler(IServiceManager serviceManager) : IRequestHandler<ResolveDuplicationCommand, Result>
    {
        private readonly IServiceManager _serviceManager = serviceManager;

        public async Task<Result> Handle(ResolveDuplicationCommand request, CancellationToken cancellationToken)
        {
            return _serviceManager.TagService.ResolveDuplication(request.Id,request.Status);
        }
    }
}
