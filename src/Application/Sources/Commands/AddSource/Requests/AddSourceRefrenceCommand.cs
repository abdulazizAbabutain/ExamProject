using Application.Commons.Models.Commands;

namespace Application.Sources.Commands.AddSource.Requests
{
    public class AddSourceReferenceCommand
    {
        public string? Notes { get; set; }
        public List<AddMetadataCommand> Metadata { get; set; }
    }
}
