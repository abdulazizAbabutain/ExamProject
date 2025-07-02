using Application.Commons.Models.Results;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Sources.Commands.RemoveBulkTagFromSource;

public class RemoveBulkTagFromSourceCommand : IRequest<Result<PartialsSuccessResult>>
{
    [JsonIgnore]
    public Guid SourceId { get; set; }
    public IEnumerable<Guid> TagIds { get; set; }
}
