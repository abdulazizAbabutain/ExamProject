using Application.Commons.Models.Commands;
using Domain.Enums;
using MediatR;

namespace Application.Sources.Commands.AddSource.Requests
{
    public class AddSourceCommand : IRequest
    {
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public bool HasAttachment { get; set; }
        public string? FileExtension { get; set; }
        public List<Guid>? Tags { get; set; }
        public List<AddMetadataCommand>? Metadata { get; set; }
        public List<AddSourceReferenceCommand>? References { get; set; }
    }
}
