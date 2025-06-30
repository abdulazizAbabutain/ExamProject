using Application.Commons.SharedModelResult.Source;
using Application.Sources.Commands.AddSource.Requests;
using Domain.Enums;

namespace Application.Sources.Commands.AddSource.Results
{
    public class AddSourceCommandResult
    {
        public Guid Id { get; set; }
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public bool HasAttachment { get; set; }
        public string? FileExtension { get; set; }
        public IEnumerable<Guid>? Tags { get; set; }
        public IEnumerable<MetadataResult>? Metadata { get; set; }
        public IEnumerable<AddSourceReferenceCommand>? References { get; set; }
    }
}
