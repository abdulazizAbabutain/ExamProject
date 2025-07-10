using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Tags.Commands.AddTag
{
    public class AddTagCommandHandler(IServiceManager serviceManager, IMapper mapper) : IRequestHandler<AddTagCommand, Result<AddTagCommandResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AddTagCommandResult>> Handle(AddTagCommand request, CancellationToken cancellationToken)
        {
            var serviceResult = _serviceManager.TagService.AddTag(request.Name, request.BackgroundColorCode,request.TextColorCode,request.IconCommand);

            if (serviceResult.IsSuccess)
                return Result<AddTagCommandResult>.CreatedSuccess(_mapper.Map<AddTagCommandResult>(serviceResult.Value));
            else
                return Result<AddTagCommandResult>.Failure(serviceResult.Errors,serviceResult.StatusCode);
        }
    }
}
