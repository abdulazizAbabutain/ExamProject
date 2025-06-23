using Application.Sources.Queries.GetAllSources;
using Domain.Lookups;

namespace Application.Sources.Queries.GetSourceById
{
    public class GetSourceByIdQueryResult
    {
        public Guid Id { get; set; }
        public SourceTypeLookup Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string URL { get; set; }
        public List<TagDto>? Tags { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int VersionNumber { get; set; }
    }
}
