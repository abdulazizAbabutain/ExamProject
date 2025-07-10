using Domain.Enums;

namespace Domain.Entities.Metadata;

public class IconMetadata
{
    public IconMetadata(string name, string url, string color)
    {
        Name = name;
        Url = url;
        Color = color;
    }
    public string Name { get; set; }
    public string Url { get; set; }
    public string Color { get; set; }
}
