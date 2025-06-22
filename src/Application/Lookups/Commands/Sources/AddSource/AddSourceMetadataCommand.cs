using Domain.Enums;

namespace Application.Lookups.Commands.Sources.AddSource
{
    public class AddSourceMetadataCommand
    {
        public string FiledName { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public FiledType FiledType { get; set; }
    }
}
