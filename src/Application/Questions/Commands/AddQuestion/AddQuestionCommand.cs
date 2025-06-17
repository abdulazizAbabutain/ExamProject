using Application.Questions.Commands.AddQuestion.CommandModels;
using Domain.Enums;
using MediatR;

namespace Application.Questions.Commands.AddQuestion;

public class AddQuestionCommand : IRequest<AddQuestionCommandResult>
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
    public required int Mark { get; set; }
    public required bool RequireManualReview { get; set; }
    public QuestionDifficulty Difficulty { get; set; }
    public IEnumerable<MultipleChoiseQuestionCommand>? Options { get; set; }
    public TrueFalseQuestionCommand? TrueAndFalseAnswer { get; set; }
    public ShortAnswerQuestionCommand? ShortAnswer { get; set; }
    public LongAnswerQuestionCommand? LongAnswer { get; set; }
    public IEnumerable<ReorderingQuestionCommand>? Reordering { get; set; }
    #region refrence 
    public Guid? Language { get; set; }
    public List<Guid>? Tags { get; set; }
    public List<Guid>? Sources { get; set; }
    public Guid? Category { get; set; }
    #endregion
}
