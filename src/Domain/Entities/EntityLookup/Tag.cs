using Domain.Auditing;
using Domain.Constants;
using Domain.Enums;
using Domain.Extentions;

namespace Domain.Entities.EntityLookup
{
    public class Tag : EntityAudit
    {
        private Tag() {}
        public Tag(string name, string backgroundColorHexCode, string textColorCode = null) 
        {
            var textColor = textColorCode ?? ColorsConsts.White;
            Id = Guid.CreateVersion7();
            Name = name;
            BackgroundColorCode = backgroundColorHexCode;
            BackgroundColorGroup = backgroundColorHexCode.GetColorGroup();
            TextColorCode = textColor;
            TextColorGroup = textColor.GetColorGroup();
            Created();
        }


        public void UpdateTag(string name, string backgroundColorHexCode, string textColorCode)
        {
            Name = name;
            BackgroundColorCode = backgroundColorHexCode;
            BackgroundColorGroup = backgroundColorHexCode.GetColorGroup();
            TextColorCode = textColorCode;
            Updated();
        }
        public void ArchiveTag() => Archive();

        public void UnArchiveTag() => UnArchive();

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string BackgroundColorCode { get; private set; }
        public string TextColorCode { get; private set; }

        public ColorCategory BackgroundColorGroup { get; private set; }
        public ColorCategory TextColorGroup { get; private set; }
    }
}
