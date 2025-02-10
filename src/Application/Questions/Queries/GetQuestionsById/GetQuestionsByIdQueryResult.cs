using Application.Questions.Queries.GetQuestionsById.ResultModel;
using Domain.Enums;

namespace Application.Questions.Queries.GetQuestionsById;

public class GetQuestionsByIdQueryResult
{
    public Guid Id { get; set; }
    public string QuestionText { get; set; }
    public QuestionType QuestionType { get; set; }
    public int Mark { get; set; }
    public bool RequireManulReview { get; set; }
    public List<string> Tags { get; set; }
    public MultipleChoiseQuestionResult? MultipleChoiseQuestion { get; set; }
}
