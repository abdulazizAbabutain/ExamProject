using Application.Questions.Commands.AddQuestion.CommandModels;
using Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;

namespace Application.Questions.Commands.AddQuestion;

public class AddQuestionCommand : IRequest<AddQuestionCommandResult>
{
    /// <summary>
    /// Question text.
    /// </summary>
    [SwaggerSchema("Question text for displaying the queastion.")]
    public required string QuestionText { get; set; }
    /// <summary>
    /// Question Type. witch can be the following 
    /// 1- Multiple Choise. 
    /// 2- True and false.
    /// </summary>
    [SwaggerSchema("The queastion type for the queastion.")]
    public required QuestionType QuestionType { get; set; }
    public required int Mark { get; set; }
    public required bool RequireManulReview { get; set; }
    public IEnumerable<MultipleChoiseQuestionCommand>? Options { get; set; }
    public TrueFalseQuestionCommand? TrueAndAnswer { get; set; }

}
