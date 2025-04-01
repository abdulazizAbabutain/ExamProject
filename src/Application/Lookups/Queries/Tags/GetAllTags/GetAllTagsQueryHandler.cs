using Application.Commons.Managers;
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
            Name = t.Name
        }).ToList();

        _serviceManager.Dispose();
        return tags;
    }
}
