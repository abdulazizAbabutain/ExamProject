using Application.Commons.SharedModelResult.Source;
using Domain.Enums;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ColorHexCode { get; set; }
    public ColorCategory ColorGroup { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public DateTimeOffset? LastArchiveDate { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsArchived { get; set; }
    public int VersionNumber { get; set; }
    public IEnumerable<SourceResult> RelatedSources { get; set; }
}