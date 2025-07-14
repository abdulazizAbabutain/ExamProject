using Application.Commons.SharedModelResult.Icons;
using Domain.Enums;

namespace Application.Tags.Commands.AddTag
{
    public class AddTagCommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public EntityLanguage Language { get; set; }
        public string BackgroundColorCode { get; set; }
        public string TextColorCode { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public int VersionNumber { get; set; }
        public IconMetadataResult? Icon { get; set; }

    }
}
