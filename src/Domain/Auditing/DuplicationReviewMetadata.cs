using Domain.Enums;

namespace Domain.Auditing
{
    public class DuplicationReviewMetadata
    {
        public bool IsDuplicated { get; private set; } = false;
        public ReviewDuplicationStatus ReviewDuplicationStatus { get; private set; } = ReviewDuplicationStatus.Pending;
        public List<Guid>? DuplicateOf { get; private set; }
        public DateTimeOffset? ReviewedAt { get; private set; }
        public DateTimeOffset? DetectedAt { get; private set; }

        private DuplicationReviewMetadata() { }

        public static DuplicationReviewMetadata CreateDetected(List<Guid> duplicates)
            => new()
            {
                IsDuplicated = true,
                ReviewDuplicationStatus = ReviewDuplicationStatus.Pending,
                DuplicateOf = duplicates,
                DetectedAt = DateTimeOffset.UtcNow
            };

        public DuplicationReviewMetadata MarkReviewed(ReviewDuplicationStatus status)
        {
            IsDuplicated = status == ReviewDuplicationStatus.Confirmed;
            ReviewDuplicationStatus = status;
            ReviewedAt = DateTimeOffset.UtcNow;
            return this;
        }
    }
}
