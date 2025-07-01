using API.Interfaces;
using Application.Categories.Commands.AddCategory;
using Application.Categories.Queries.GetAllCategory;
using Application.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [Route("api/category")]
    public class CategoryController(IMediator mediator, IHttpResultResponder httpResultResponder) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IHttpResultResponder _httpResultResponder = httpResultResponder;

        [HttpPost(Name = nameof(AddCateogry))]
        public async Task<IActionResult> AddCateogry([FromBody] AddCategoryCommand command)
        {
            return _httpResultResponder.FromResult(HttpContext,await _mediator.Send(command));
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
