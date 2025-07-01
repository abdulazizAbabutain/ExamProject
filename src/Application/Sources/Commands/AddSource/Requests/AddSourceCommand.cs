using Application.Commons.Models.Commands;
using Application.Commons.Models.Results;
using Application.Sources.Commands.AddSource.Results;
using Domain.Enums;
using MediatR;

namespace Application.Sources.Commands.AddSource.Requests
{
    public class AddSourceCommand : IRequest<Result<AddSourceCommandResult>>
    {
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public bool HasAttachment { get; set; }
        public string? FileExtension { get; set; }
        public List<Guid>? Tags { get; set; }
        public Guid? CategoryId { get; set; }
        public List<AddMetadataCommand>? Metadata { get; set; }
        public List<AddSourceReferenceCommand>? References { get; set; }
    }
}
