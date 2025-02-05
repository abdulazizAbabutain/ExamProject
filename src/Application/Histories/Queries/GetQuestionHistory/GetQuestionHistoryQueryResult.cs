using Domain.Enums;

namespace Application.Histories.Queries.GetQuestionHistory
{
    internal class GetQuestionHistoryQueryResult
    {
        public Guid Id { get; set; }
        public required string QuestionText { get; set; }
        public required QuestionType QuestionType { get; set; }
        public required int Mark { get; set; }
        public required bool RequireManulReview { get; set; }
        public Guid QuestionId { get; set; }
        public DateTime ActionDate { get; set; }
        public EntityHistoryType ActionType { get; set; }
        public int VerstionNumber { get; set; }
    }
}
