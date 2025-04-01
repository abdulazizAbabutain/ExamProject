using Application.Questions.Queries.GetQuestionsById.ResultModel;
using Domain.Lookups;
using LiteDB;

namespace Application.Questions.Queries.GetQuestionsById;

public class GetQuestionsByIdQueryResult
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public List<string>? Variants { get; set; }
    public QuestionTypeLookup QuestionType { get; set; }
    public QuestionDifficultyLookup Difficulty { get; set; }
    public int VersionNumber { get; set; }
    public int Mark { get; set; }
    public bool RequireManulReview { get; set; }
    public List<string> Tags { get; set; }
    public List<QuestionSourceResult>? Sources {  get; set; }
    public IEnumerable<MultipleChoiseQuestionResult>? MultipleChoiseOptions { get; set; }
    public TrueFalseQuestionQueryResult? TrueAndFalse { get; set; }
}
