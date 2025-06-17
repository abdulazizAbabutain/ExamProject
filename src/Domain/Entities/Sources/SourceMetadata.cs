using Domain.Enums;

namespace Domain.Entities.Sources
{
    public class SourceMetadata
    {
        public Guid Id { get; set; }
        public string FiledName { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public FiledType FiledType { get; set; }
    }
}
