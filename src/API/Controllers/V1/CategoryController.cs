using API.Interfaces;
using Application.Categories.Commands.AddCategory;
using Application.Categories.Commands.UpdateCategory;
using Application.Categories.Queries.GetAllCategory;
using Application.Categories.Queries.GetCategoryById;
using Application.EntitlesTimeline.Queries.EntityTimeline;
using Application.EntitlesTimeline.Queries.EntityTimelineDetails;
using Domain.Enums;
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


        [HttpPut("{categoryId:guid}", Name = nameof(UpdateCategory))]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId ,[FromBody] UpdateCategoryCommand command)
        {
            command.CategoryId = categoryId;
            return _httpResultResponder.FromResult(HttpContext, await _mediator.Send(command));
        }

        [HttpPut("{categoryId:guid}/parent-reference", Name = nameof(UpdateParrentRefrence))]
        public async Task<IActionResult> UpdateParrentRefrence([FromRoute] Guid categoryId, [FromBody] UpdateCategoryCommand command)
        {
            command.CategoryId = categoryId;
            return _httpResultResponder.FromResult(HttpContext, await _mediator.Send(command));
        }

        [HttpGet("{categoryId:guid}/timeline", Name = nameof(GetCategoryTimeline))]
        [EndpointName(nameof(GetCategoryTimeline))]
        [EndpointSummary("Tag Timeline")]
        [EndpointDescription("get the timeline for tag")]
        public async Task<IActionResult> GetCategoryTimeline([FromRoute] Guid categoryId, [FromQuery] GetEntityTimelineQuery query)
        {
            query.Id = categoryId;
            query.EntityName = EntityName.Category;
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{categoryId:guid}/timeline/{timelineId:guid}", Name = nameof(GetCategoryTimelineDetails))]
        [EndpointName(nameof(GetCategoryTimelineDetails))]
        [EndpointSummary("Tag Timeline details")]
        [EndpointDescription("get the timeline for tag")]
        public async Task<IActionResult> GetCategoryTimelineDetails([FromRoute(Name = "categoryId")] Guid categoryId, [FromRoute(Name = "TimelineId")] Guid timelineId)
        {
            var query = new EntityTimelineDetailsQuery()
            {
                TimelineId = timelineId,
                EntityName = EntityName.Category,
                EntityId = categoryId
            };
            return Ok(await _mediator.Send(query));
        }

    }
}
