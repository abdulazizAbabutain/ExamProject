using Domain.Enums;
using MediatR;

namespace Application.Lookups.Commands.Sources.AddSource
{
    public class AddSourceCommand : IRequest
    {
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<Guid>? Tags { get; set; }
        public List<AddSourceMetadataCommand> Metadata { get; set; }
    }
}
