using Application.Commons.Models.Pageination;
using Application.Lookups.Commands.Categories.AddCategory;
using Application.Lookups.Commands.Languages;
using Application.Lookups.Commands.Sources.AddSource;
using Application.Lookups.Commands.Sources.UpdateSource;
using Application.Lookups.Commands.Tags.AddTag;
using Application.Lookups.Queries.Languages.GetLanguages;
using Application.Lookups.Queries.Sources.GetAllSources;
using Application.Lookups.Queries.Sources.GetSourceById;
using Application.Lookups.Queries.Tags.GetAllTags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly IMediator mediator;

        public LookupController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("language", Name = nameof(AddLangugue))]
        public async Task<IActionResult> AddLangugue([FromBody] AddLanguageCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }



        [HttpGet("language", Name = nameof(AddLangugue))]
        public async Task<IActionResult> GetLangugues()
        {
            return Ok(await mediator.Send(new GetLanguagesQuery()));
        }



        [HttpPost("tag", Name = nameof(AddTag))]
        public async Task<IActionResult> AddTag([FromBody] AddTagCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }


        [HttpGet("tag", Name = nameof(GetAllTags))]
        public async Task<IActionResult> GetAllTags()
        {
            return Ok(await mediator.Send(new GetAllTagsQuery()));

        }

        [HttpPost("source", Name = nameof(AddSource))]
        public async Task<IActionResult> AddSource([FromBody] AddSourceCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("source", Name = nameof(GetAllSources))]
        public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourceQuery query)
        {
            return Ok(await mediator.Send(query));

        }

        [HttpGet("source/{id}", Name = nameof(GetSourceById))]
        public async Task<IActionResult> GetSourceById([FromRoute] GetSourceByIdQuery query)
        {
            return Ok(await mediator.Send(query));

        }


        [HttpPut("source", Name = nameof(UpdateSource))]
        public async Task<IActionResult> UpdateSource([FromBody] UpdateSourceCommand command)
        {
            await mediator.Send(command);
            return NoContent();

        }

        [HttpPost("category", Name = nameof(AddCateogry))]
        public async Task<IActionResult> AddCateogry([FromBody] AddCategoryCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }



    }
}
