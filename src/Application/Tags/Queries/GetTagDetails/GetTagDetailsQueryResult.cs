using Application.Commons.SharedModelResult.Icons;
using Domain.Enums;

namespace Application.Tags.Queries.GetTagDetails;

public class GetTagDetailsQueryResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string? Description { get; set; }
    public string BackgroundColorCode { get; set; }
    public string TextColorCode { get; set; }
    public DateTimeOffset? LastModifiedDate { get; set; }
    public DateTimeOffset? LastArchiveDate { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public bool IsArchived { get; set; }
    public bool NeedReview { get; set; }
    public int VersionNumber { get; set; }
    public IconMetadataResult? Icon { get; set; }
}