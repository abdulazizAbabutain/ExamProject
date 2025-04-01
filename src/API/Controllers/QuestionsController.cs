using Application.Histories.Queries.GetQuestionHistory;
using Application.Questions.Commands.AddQuestion;
using Application.Questions.Commands.DeleteAllQuestion;
using Application.Questions.Commands.DeleteQuestion;
using Application.Questions.Commands.UpdateQuestion;
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
        return Ok(await mediator.Send(command));
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

    [HttpPut( Name = nameof(UpdateQuestion))]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

}
