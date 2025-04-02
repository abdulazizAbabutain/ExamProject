using Domain.Enums;

namespace Domain.Lookups
{
    public class ColorCategoryLookup
    {
        public ColorCategoryLookup(ColorCategory id)
        {
            Id = id;
        }
        public ColorCategory Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case ColorCategory.Red:
                        return nameof(ColorCategory.Red);
                    case ColorCategory.Orange:
                        return nameof(ColorCategory.Orange);
                    case ColorCategory.Yellow:
                        return nameof(ColorCategory.Yellow);
                    case ColorCategory.Green:
                        return nameof(ColorCategory.Green);
                    case ColorCategory.Cyan:
                        return nameof(ColorCategory.Cyan);
                    case ColorCategory.Blue:
                        return nameof(ColorCategory.Blue);
                    case ColorCategory.Purple:
                        return nameof(ColorCategory.Purple);
                    case ColorCategory.Pink:
                        return nameof(ColorCategory.Pink);
                    
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
