using MediatR;

namespace Application.Lookups.Queries.Tags.GetAllTags
{
    public class GetAllTagsQuery : IRequest<IEnumerable<GetAllTagsQueryResult>>
    {
    }
}
