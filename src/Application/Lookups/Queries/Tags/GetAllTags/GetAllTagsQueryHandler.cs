using Application.Commons.Managers;
using Application.Commons.Models.Pageination;
using Domain.Entities.EntityLookup;
using Domain.Lookups;
using Domain.Managers;
using LinqKit;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags;

public class GetAllTagsQueryHandler(IRepositoryManager repositoryManager) : IRequestHandler<GetAllTagsQuery, PageResponse<GetAllTagsQueryResult>>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;

    public async Task<PageResponse<GetAllTagsQueryResult>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var query = PredicateBuilder.New<Tag>(true);

        if (request.ColorCategory.HasValue)
            query = query.And(e => e.ColorGroup.Equals(request.ColorCategory));

        if(!string.IsNullOrEmpty(request.Search))
            query = query.And(e => e.Name.Contains(request.Search));


        var tags = _repositoryManager.TagRepository.GetAll(query, request.PageNumber,request.PageSize);

        var result = tags.Select(t => new GetAllTagsQueryResult
        {
            Id = t.Id,
            Name = t.Name,
            ColorCode  = t.ColorHexCode,
            ColorCategory = new ColorCategoryLookup(t.ColorGroup)
        }).ToList();

        var count = _repositoryManager.TagRepository.Count();
        
        return new PageResponse<GetAllTagsQueryResult>(result,request.PageNumber,request.PageSize, count);
    }
}
