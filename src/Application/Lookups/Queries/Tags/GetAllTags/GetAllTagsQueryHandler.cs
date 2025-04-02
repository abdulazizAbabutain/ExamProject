using Application.Commons.Managers;
using Domain.Lookups;
using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags;

public class GetAllTagsQueryHandler(IServiceManager serviceManager) : IRequestHandler<GetAllTagsQuery, IEnumerable<GetAllTagsQueryResult>>
{
    private readonly IServiceManager _serviceManager = serviceManager;

    public async Task<IEnumerable<GetAllTagsQueryResult>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = _serviceManager.LookupService.GetAllTags().Select(t => new GetAllTagsQueryResult
        {
            Id = t.Id,
            Name = t.Name,
            ColorCode = t.ColorHexCode,
            ColorCategory = new ColorCategoryLookup(t.ColorGroup)
            
        }).ToList();

        _serviceManager.Dispose();
        return tags;
    }
}
