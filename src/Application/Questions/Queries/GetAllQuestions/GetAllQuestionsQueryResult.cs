using Application.Commons.SharedModelResult;
using Domain.Enums;
using Domain.Lookups;

namespace Application.Questions.Queries.GetAllQuestions;

public class GetAllQuestionsQueryResult
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public int Mark { get; set; }
    public bool RequireManualReview { get; set; }
    public string? Category { get; set; }
    public QuestionDifficulty Difficulty { get; set; }
    public IEnumerable<TagResult>? Tags { get; set; }
}
