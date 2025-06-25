using Application.Questions.Commands.AddQuestion;
using Application.Questions.Commands.DeleteAllQuestion;
using Application.Questions.Commands.DeleteQuestion;
using Application.Questions.Commands.UpdateQuestion;
using Application.Questions.Queries.GetAllQuestions;
using Application.Questions.Queries.GetQuestionsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1;
/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IMediator mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    public QuestionsController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddQuestion(AddQuestionCommand command)
    {
        return Ok(await mediator.Send(command));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllQuestions([FromQuery] GetAllQuestionsQuery query)
    {
        return Ok(await mediator.Send(query));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById([FromRoute] Guid id)
    {
        return Ok(await mediator.Send(new GetQuestionsByIdQuery { Id = id }));
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpDelete(Name = nameof(DeleteAllQuestion))]
    public async Task<IActionResult> DeleteAllQuestion()
    {
        await mediator.Send(new DeleteAllQuestionCommand());
        return NoContent();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}", Name = nameof(DeleteQuestion))]
    public async Task<IActionResult> DeleteQuestion([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteQuestionCommand { Id = id });
        return NoContent();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut(Name = nameof(UpdateQuestion))]
    public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}
