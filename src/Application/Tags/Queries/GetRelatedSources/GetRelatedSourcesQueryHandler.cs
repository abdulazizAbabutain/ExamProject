using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Entities.Sources;
using Domain.Extentions;
using Domain.Managers;
using LinqKit;
using MapsterMapper;
using MediatR;

namespace Application.Tags.Queries.GetRelatedSources;

public class GetRelatedSourcesQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) 
    : IRequestHandler<GetRelatedSourcesQuery, Result<PageResponse<GetRelatedSourcesQueryResult>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<Result<PageResponse<GetRelatedSourcesQueryResult>>> Handle(GetRelatedSourcesQuery request, CancellationToken cancellationToken)
    {
        if (_repositoryManager.TagRepository.IsNotExist(request.TagId))
            return Result<PageResponse<GetRelatedSourcesQueryResult>>.NotFoundFailure(nameof(request.TagId),$"tag with Id {request.TagId} was not found");

        var query = PredicateBuilder.New<Source>(true);

        query = query.And(e => e.Tags.IsNotNull() && e.Tags.Contains(request.TagId));

        if (request.TypeId.HasValue)
            query = query.And(e => e.Type == request.TypeId);

        if(!string.IsNullOrWhiteSpace(request.Title))
            query = query.And(q => q.Title.ToLower().Contains(request.Title.ToLower()));

        var sources = _repositoryManager.SourceRepository.GetAll(query,request.PageNumber,request.PageSize);
        var count = _repositoryManager.SourceRepository.CountBy(e => e.Tags.IsNotNull() && e.Tags.Contains(request.TagId));

        return Result<PageResponse<GetRelatedSourcesQueryResult>>.Success(new PageResponse<GetRelatedSourcesQueryResult>(_mapper.Map<IEnumerable<GetRelatedSourcesQueryResult>>(sources), request.PageNumber, request.PageSize, count));

    }
}
