using Domain.Enums;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryResult
{
    public Guid Id { get; set; }
    public required string QuestionText { get; set; }
    public required QuestionType QuestionType { get; set; }
    public required int Mark { get; set; }
    public required bool RequireManulReview { get; set; }
}
