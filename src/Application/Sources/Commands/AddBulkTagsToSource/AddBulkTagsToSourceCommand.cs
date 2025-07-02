using Application.Commons.Models.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Sources.Commands.AddBulkTagsToSource;

public class AddBulkTagsToSourceCommand : IRequest<Result<PartialsSuccessResult>>
{
    [JsonIgnore]
    public Guid SourceId { get; set; }
    public IEnumerable<Guid> TagIds { get; set; }
}
