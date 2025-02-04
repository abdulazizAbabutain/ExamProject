using Domain.Enums;

namespace Application.Questions.Queries.GetQuestionsById;

public class GetQuestionsByIdQueryResult
{
    public required Guid Id { get; set; }
    public required string QuestionText { get; set; }
    public required QuestionType QuestionType { get; set; }
    public required int Mark { get; set; }
    public required bool RequireManulReview { get; set; }
}
