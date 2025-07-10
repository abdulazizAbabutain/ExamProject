using Domain.Enums;

namespace Application.Commons.Models.Icons;

public class IconsModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Url { get; set; } = default!;
    public IconSource Source { get; set; }
}
