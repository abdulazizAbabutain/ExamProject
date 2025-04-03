using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.EntityLookup
{
    public class Tag 
    {          
        public Tag(string name, string colorHexCode) 
        {
            Id = Guid.CreateVersion7();
            Name = name;
            ColorHexCode = colorHexCode;
            ColorGroup = colorHexCode.GetColorGroup();
        }


        public void UpdateTag(string name, string colorHexCode)
        {
            Name = name;
            ColorHexCode = colorHexCode;
            ColorGroup = colorHexCode.GetColorGroup();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ColorHexCode { get; private set; }
        public ColorCategory ColorGroup { get; private set; }
    }
}
