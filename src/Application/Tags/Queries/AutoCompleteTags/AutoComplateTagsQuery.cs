using Application.Commons.Models.Results;
using MediatR;

namespace Application.Tags.Queries.AutoCompleteTags;

public class AutoCompleteTagsQuery : IRequest<Result<IEnumerable<AutoCompleteTagsQueryResult>>>
{
    public string Name { get; set; }
}
