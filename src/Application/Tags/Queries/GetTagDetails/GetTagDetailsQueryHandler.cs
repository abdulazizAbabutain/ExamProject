using Application.Commons.Managers;
using Application.Commons.Models.Results;
using Application.Commons.SharedModelResult.Source;
using Domain.Extentions;
using Domain.Managers;
using MapsterMapper;
using MediatR;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetTagDetailsQuery, Result<GetTagDetailsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetTagDetailsQueryResult>> Handle(GetTagDetailsQuery request, CancellationToken cancellationToken)
    {
        var tag = _repositoryManager.TagRepository.GetById(request.Id);
        if (tag.IsNull())
            return Result<GetTagDetailsQueryResult>.NotFoundFailure(nameof(request.Id), $"tag with an {request.Id} was not found");

        return Result<GetTagDetailsQueryResult>.Success(_mapper.Map<GetTagDetailsQueryResult>(tag));
    }
}