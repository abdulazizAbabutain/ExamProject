using Domain.Entities.Sources;

namespace Application.Sources.Queries.GetSourceById.ResultModels
{
    public class SourceReferenceResult
    {
        public Guid Id { get; set; }
        public string? Notes { get; set; }
        public List<MetadataResult>? Metadata { get; set; }
    }
}
