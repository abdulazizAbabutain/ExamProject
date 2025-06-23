using Domain.Enums;

namespace Domain.Entities.Sources
{
    public class Metadata
    {
        public Metadata(string filedName, string value, bool isRequired, FiledType filedType)
        {
            Id = Guid.CreateVersion7();
            FiledName = filedName;
            Value = value;
            IsRequired = isRequired;
            FiledType = filedType;
        }
        public Guid Id { get; set; }
        public string FiledName { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public FiledType FiledType { get; set; }
    }
}
