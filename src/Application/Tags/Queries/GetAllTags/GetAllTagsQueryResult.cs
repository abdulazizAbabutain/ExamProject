using Domain.Enums;
using Domain.Lookups;

namespace Application.Lookups.Queries.Tags.GetAllTags
{
    public class GetAllTagsQueryResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public ColorCategory ColorCategory { get; set; }
        public bool IsArchived { get; set; }
    }
}
