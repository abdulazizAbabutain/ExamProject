using Application.Lookups.Commands.Categories.AddCategory;
using Application.Lookups.Queries.Categories.GetAllCategory;
using Application.Lookups.Queries.Categories.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost(Name = nameof(AddCateogry))]
        public async Task<IActionResult> AddCateogry([FromBody] AddCategoryCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet(Name = nameof(GetAllCategory))]
        public async Task<IActionResult> GetAllCategory([FromQuery] GetAllCategoryQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id:guid}", Name = nameof(GetCategoryById))]
        public async Task<IActionResult> GetCategoryById([FromRoute] GetCategoryByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
