using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.Models.ServicesModel.Source;
using Application.Sources.Commands.AddSource.Requests;
using Application.Sources.Commands.AddSource.Results;
using Domain.Entities.Sources;
using Domain.Extentions;
using MapsterMapper;
using MediatR;

namespace Application.Sources.Commands.AddSource
{
    public class AddSourceCommandHandler(IServiceManager serviceManager, IMapper mapper) : IRequestHandler<AddSourceCommand, Result<AddSourceCommandResult>>
    {
        private readonly IServiceManager _serviceManager = serviceManager;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<AddSourceCommandResult>> Handle(AddSourceCommand request, CancellationToken cancellationToken)
        {
            var sourceServiceModel = _mapper.Map<AddSourceServiceModel>(request);
            var sourceResult = _serviceManager.SourceService.AddSource(sourceServiceModel);

            if (!sourceResult.IsSuccess)
                return Result<AddSourceCommandResult>.Failure(sourceResult.Errors, sourceResult.StatusCode);

            List<SourceReference> references = new(); 

            if (request.References.IsNotNull())
            {
                var referenceServiceModel = _mapper.Map<IEnumerable<AddSourceReferenceServiceModel>>(request.References);
                var referenceResult = _serviceManager.SourceService.AddReference(referenceServiceModel, sourceResult.Value.Id);

                if (!referenceResult.IsSuccess)
                {
                    return Result<AddSourceCommandResult>.Failure(referenceResult.Errors, referenceResult.StatusCode);
                }

                references = referenceResult.Value?.ToList() ?? new();
            }

            var sourceToReturn = _mapper.Map<AddSourceCommandResult>(sourceResult.Value);
            sourceToReturn.References = request.References.IsNotNull() ? _mapper.Map<IEnumerable<AddSourceReferenceCommand>>(references) : null;

            return Result<AddSourceCommandResult>.CreatedSuccess(sourceToReturn);
        }
    }
}
