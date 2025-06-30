using Domain.Enums;

namespace Application.Commons.SharedModelResult.Source
{
    public class MetadataResult
    {
        public Guid Id { get; set; }
        public string FiledName { get; set; }
        public string Value { get; set; }
        public FiledType FiledType { get; set; }
    }
}
