using Domain.Enums;

namespace Application.Tags.Commands.AddTag
{
    public class AddTagCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColorCode { get; set; }
        public string TextColorCode { get; set; }
        public ColorCategory BackgroundColorGroup { get; set; }
        public ColorCategory TextColorGroup { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int VersionNumber { get; set; }
    }
}
