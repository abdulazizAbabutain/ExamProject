using Application.Questions.Commands.AddQuestion.CommandModels;
using Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using Application.Questions.Queries.GetQuestionsById;

namespace Application.Questions.Commands.AddQuestion;

public class AddQuestionCommand : IRequest<GetQuestionsByIdQueryResult>
{
    /// <summary>
    /// Question text.
    /// </summary>
    public required string QuestionText { get; set; }
    /// <summary>
    /// Question Type. witch can be the following 
    /// 1- Multiple Choise. 
    /// 2- True and false.
    /// </summary>
    public required QuestionType QuestionType { get; set; }
    /// <summary>
    /// List of the same question but different style 
    /// </summary>
    public List<string>? Variants { get; set; }
    public string LanguageCode { get; set; }
    public required int Mark { get; set; }
    public required bool RequireManulReview { get; set; }
    public List<Guid>? Tags { get; set; }
    public QuestionDifficulty Difficulty { get; set; }
    public List<QuestionSourceCommand> Sources { get; set; }
    public IEnumerable<MultipleChoiseQuestionCommand>? Options { get; set; }
    public TrueFalseQuestionCommand? TrueAndAnswer { get; set; }

}
