using Application.Questions.Commands.AddQuestion;
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
    public async Task<IActionResult> GetAllQuestions(GetAllQuestionsQuery query)
    {
        return Ok(await mediator.Send(query));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllQuestions([FromRoute] Guid id)
    {
        return Ok(await mediator.Send(new GetQuestionsByIdQuery { Id = id}));
    }
}
