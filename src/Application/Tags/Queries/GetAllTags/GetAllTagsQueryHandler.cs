using Application.Commons.Models.Pageination;
using Application.Commons.Models.Results;
using Domain.Entities.EntityLookup;
using Domain.Extentions;
using Domain.Managers;
using LinqKit;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags;

public class GetAllTagsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetAllTagsQuery, Result<PageResponse<GetAllTagsQueryResult>>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<Result<PageResponse<GetAllTagsQueryResult>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var query = PredicateBuilder.New<Tag>(true);

        if (request.ColorCategory.HasValue)
            query = query.And(e => e.ColorGroup.Equals(request.ColorCategory));

        if(!string.IsNullOrEmpty(request.Search))
            query = query.And(e => e.Name.ToLower().Contains(request.Search.ToLower()));


        var tags = _repositoryManager.TagRepository.GetAll(query, request.PageNumber,request.PageSize).Where(t => t.IsArchived == request.IsArchived).ToList();
        var count = _repositoryManager.TagRepository.Count();

        if (tags.IsNull())
            return Result<PageResponse<GetAllTagsQueryResult>>.Success(new PageResponse<GetAllTagsQueryResult>(request.PageNumber, request.PageSize, count));


        var result = tags.Select(t => new GetAllTagsQueryResult
        {
            Id = t.Id,
            Name = t.Name,
            ColorCode  = t.ColorHexCode,
            ColorCategory = t.ColorGroup
        }).ToList();

        return Result<PageResponse<GetAllTagsQueryResult>>.Success(new PageResponse<GetAllTagsQueryResult>(result,request.PageNumber, request.PageSize, count));
    }
}
