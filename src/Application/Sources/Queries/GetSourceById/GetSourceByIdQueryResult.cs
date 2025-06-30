using Application.Commons.SharedModelResult;
using Application.Commons.SharedModelResult.Source;
using Domain.Enums;

namespace Application.Sources.Queries.GetSourceById
{
    public class GetSourceByIdQueryResult
    {
        public Guid Id { get; set; }
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool HasAttachment { get; set; }
        public string? FileExtension { get; set; }
        public string? FilePath { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public DateTimeOffset? LastArchiveDate { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public bool IsArchived { get; set; }
        public int VersionNumber { get; set; }
        public IEnumerable<TagResult>? Tags { get; set; }
        public IEnumerable<TagResult>? ArchivedTags { get; set; }
        public IEnumerable<MetadataResult>? Metadata { get; set; }
    }
}
