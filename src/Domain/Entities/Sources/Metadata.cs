using Domain.Enums;

namespace Domain.Entities.Sources
{
    public class Metadata
    {
        private Metadata()
        {
            
        }
        public Metadata(string filedName, string value, FiledType filedType)
        {
            Id = Guid.CreateVersion7();
            FiledName = filedName;
            Value = value;
            FiledType = filedType;
        }
        public Guid Id { get; set; }
        public string FiledName { get; set; }
        public string Value { get; set; }
        public FiledType FiledType { get; set; }
    }
}
