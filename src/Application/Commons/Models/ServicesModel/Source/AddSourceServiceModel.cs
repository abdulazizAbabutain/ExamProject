using Domain.Enums;

namespace Application.Commons.Models.ServicesModel.Source
{
    public class AddSourceServiceModel
    {
        public SourceType Type { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? FilePath { get; set; }
        public bool HasAttachment { get; set; }
        public string? FileExtension { get; set; }
        public List<Guid>? Tags { get; set; }
        public List<AddMetadataSourceModel>? Metadata { get; set; }
    }
}
