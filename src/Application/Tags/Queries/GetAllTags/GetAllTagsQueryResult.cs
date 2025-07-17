using Domain.Enums;
using Domain.Lookups;

namespace Application.Lookups.Queries.Tags.GetAllTags
{
    public class GetAllTagsQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BackgroundColorCode { get; set; }
        public string TextColorCode { get; set; }
        public EntityLanguage Language { get; set; }
        public bool IsArchived { get; set; }
        public bool NeedReview { get; set; }
    }
}
