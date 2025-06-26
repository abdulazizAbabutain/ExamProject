using Domain.Enums;

namespace Application.Tags.Commands.AddTag
{
    public class AddTagCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ColorHexCode { get; set; }
        public ColorCategory ColorGroup { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int VersionNumber { get; set; }
    }
}
