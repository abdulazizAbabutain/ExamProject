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
        
        if(request.IsArchived.HasValue)
            query = query.And(e => e.IsArchived == request.IsArchived.Value);
        else
            query = query.And(e => !e.IsArchived );


        var tags = (await _repositoryManager.TagRepository.GetAllAsync(query, request.PageNumber,request.PageSize)).ToList();
        var count = await _repositoryManager.TagRepository.CountAsync();

        if (tags.IsNull() || !tags.Any())
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
