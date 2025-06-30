using Application.Commons.Models.Commands;
using Application.Commons.SharedModelResult.Source;
using Application.Sources.Commands.AddSource.Requests;
using Domain.Enums;

namespace Application.Sources.Commands.AddSource.Results
{
    public class SourceReferenceResult
    {
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public bool HasAttachment { get; set; }
        public string? FileExtension { get; set; }
        public List<Guid>? Tags { get; set; }
        public List<MetadataResult>? Metadata { get; set; }
        public List<AddSourceReferenceCommand>? References { get; set; }
    }
}
