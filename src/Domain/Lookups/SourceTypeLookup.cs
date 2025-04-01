using Domain.Enums;

namespace Domain.Lookups
{
    public class SourceTypeLookup
    {
        public SourceType Id { get; set; }
        public string Value
        {
            get
            {
                switch (Id)
                {
                    case SourceType.Video:
                        return nameof(SourceType.Video);
                    case SourceType.Podcast:
                        return nameof(SourceType.Podcast);
                    case SourceType.Article:
                        return nameof(SourceType.Article);
                    case SourceType.Book:
                        return nameof(SourceType.Book);
                    default:
                        throw new NotImplementedException();
                }
            }
        }
    }
}
