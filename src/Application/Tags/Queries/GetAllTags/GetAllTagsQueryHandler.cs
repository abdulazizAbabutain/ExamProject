using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Domain.Managers;
using LinqKit;
using MapsterMapper;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags;

public class GetAllTagsQueryHandler(IRepositoryManager repositoryManager, IMapper mapper) : IRequestHandler<GetAllTagsQuery, Result<PageResponse<GetAllTagsQueryResult>>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<PageResponse<GetAllTagsQueryResult>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var query = PredicateBuilder.New<Tag>(true);

        if(request.Language.HasValue)
            query = query.And(e => e.Language == request.Language);


        if (!string.IsNullOrEmpty(request.Search))
            query = query.And(e => e.Name.ToLower().Contains(request.Search.ToLower()));

        if (request.NeedReview.HasValue)
            query = query.And(e => e.DuplicationReview.IsNotNull() && e.DuplicationReview.IsDuplicated);


        if (request.IsArchived.HasValue)
            query = query.And(e => e.IsArchived == request.IsArchived.Value);
        else
            query = query.And(e => !e.IsArchived );


        var tags = _repositoryManager.TagRepository.GetAll(query, request.PageNumber,request.PageSize);
        var count = _repositoryManager.TagRepository.Count();

        if (tags.IsNull() || !tags.Any())
            return Result<PageResponse<GetAllTagsQueryResult>>.Success(new PageResponse<GetAllTagsQueryResult>(request.PageNumber, request.PageSize, count));


        return Result<PageResponse<GetAllTagsQueryResult>>.Success(new PageResponse<GetAllTagsQueryResult>(_mapper.Map<IEnumerable<GetAllTagsQueryResult>>(tags),request.PageNumber, request.PageSize, count));
    }
}
