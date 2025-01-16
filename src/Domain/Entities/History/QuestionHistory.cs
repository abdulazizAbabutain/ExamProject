using Domain.Entities.Audit;
using Domain.Enums;

namespace Domain.Entities.History
{
    public class QuestionHistory : EntityAudit
    {
        public required string QuestionText { get; set; }
        public required QuestionType QuestionType { get; set; }
        public required int Mark { get; set; }
        public required bool RequireManulReview { get; set; }
        public Guid QuestionId { get; set; }
    }
}
