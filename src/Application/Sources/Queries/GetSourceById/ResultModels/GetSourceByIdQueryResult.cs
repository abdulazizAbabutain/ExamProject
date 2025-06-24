using Application.Commons.SharedModelResult;
using Application.Sources.Queries.GetAllSources;
using Domain.Enums;

namespace Application.Sources.Queries.GetSourceById.ResultModels
{
    public class GetSourceByIdQueryResult
    {
        public Guid Id { get; set; }
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<TagResult>? Tags { get; set; }
        public List<MetadataResult>? Metadata { get; set; }
        public List<SourceReferenceResult>? References { get; set; }

    }
}
