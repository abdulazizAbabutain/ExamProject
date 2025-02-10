using Application.Histories.Queries.GetQuestionHistory;
using Application.Questions.Commands.AddQuestion;
using Application.Questions.Commands.DeleteAllQuestion;
using Application.Questions.Commands.DeleteQuestion;
using Application.Questions.Queries.GetAllQuestions;
using Application.Questions.Queries.GetQuestionsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IMediator mediator;

    public QuestionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost]
    public async Task<IActionResult> AddQuestion(AddQuestionCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQuestions([FromQuery]GetAllQuestionsQuery query)
    {
        return Ok(await mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllQuestions([FromRoute] Guid id)
    {
        return Ok(await mediator.Send(new GetQuestionsByIdQuery { Id = id}));
    }


    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetAllQuestionHistory([FromRoute] Guid id, [FromQuery] GetQuestionHistoryQuery query)
    {
        query.QuestionId = id;
        return Ok(await mediator.Send(query));
    }

    [HttpDelete(Name = nameof(DeleteAllQuestion))]
    public async Task<IActionResult> DeleteAllQuestion() 
    {
        await mediator.Send(new DeleteAllQuestionCommand());
        return NoContent();
    }


    [HttpDelete("{id}",Name = nameof(DeleteQuestion))]
    public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteQuestionCommand {  Id = id });
        return NoContent();
    }
}
