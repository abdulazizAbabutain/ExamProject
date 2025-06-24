using Application.Commons.Models.Commands;
using MediatR;

namespace Application.Sources.Commands.AddReference
{
    public class AddReferenceCommand : IRequest
    {
        public Guid SourceId { get; set; }
        public string? Notes { get; set; }
        public List<AddMetadataCommand>? Metadata { get; set; }
    }
}
