using Domain.Enums;

namespace Application.Sources.Commands.AddSource.Requests
{
    public class AddMetadataCommand
    {
        public string FiledName { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public FiledType FiledType { get; set; }
    }
}
