using Domain.Enums;

namespace Application.Sources.Queries.GetSourceById.ResultModels
{
    public class MetadataResult
    {
        public Guid Id { get; set; }
        public string FiledName { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public FiledType FiledType { get; set; }
    }
}
