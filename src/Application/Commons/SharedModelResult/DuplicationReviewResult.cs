using Domain.Enums;

namespace Application.Commons.SharedModelResult
{
    public class DuplicationReviewResult
    {
        public bool IsDuplicated { get; set; } = false;
        public ReviewDuplicationStatus ReviewDuplicationStatus { get; set; } 
        public IEnumerable<DuplicatedItem>? Items { get; set; }
        public DateTimeOffset? ReviewedAt { get; set; }
        public DateTimeOffset? DetectedAt { get; set; }
    }

    public class DuplicatedItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
