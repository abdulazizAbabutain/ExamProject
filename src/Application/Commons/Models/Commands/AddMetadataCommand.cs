using Domain.Enums;

namespace Application.Commons.Models.Commands
{
    public class AddMetadataCommand
    {
        public string FiledName { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public FiledType FiledType { get; set; }
    }
}
