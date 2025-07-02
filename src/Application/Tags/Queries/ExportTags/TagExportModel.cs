using Domain.Enums;

namespace Application.Tags.Queries.ExportTags
{
    internal class TagExportModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string ColorHexCode { get; set; } = default!;
        public ColorCategory ColorGroup { get; set; }
        public bool IsArchived { get; set; }
    }
}
