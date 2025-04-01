using Domain.Lookups;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryResult
{
    public Guid Id { get; set; }
    public required string QuestionText { get; set; }
    public required QuestionTypeLookup QuestionType { get; set; }
    public required int Mark { get; set; }
    public required bool RequireManulReview { get; set; }
    public QuestionDifficultyLookup Difficulty { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}
