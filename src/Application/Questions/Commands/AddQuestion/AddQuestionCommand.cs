using Application.Questions.Commands.AddQuestion.CommandModels;
using Domain.Enums;
using MediatR;

namespace Application.Questions.Commands.AddQuestion;

public class AddQuestionCommand : IRequest<AddQuestionCommandResult>
{
    public required string QuestionText { get; set; }
    public required QuestionType QuestionType { get; set; }
    public required int Mark { get; set; }
    public required bool RequireManulReview { get; set; }
    public IEnumerable<MultipleChoiseQuestionCommand>? Options { get; set; }

}
