using Application.Lookups.Commands.Languages;
using Application.Lookups.Queries.Languages.GetLanguages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        #region language
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
        #endregion

        //#region source 
        //[HttpPost("source", Name = nameof(AddSource))]
        //public async Task<IActionResult> AddSource([FromBody] AddSourceCommand command)
        //{
        //    await mediator.Send(command);
        //    return NoContent();
        //}

        //[HttpGet("source", Name = nameof(GetAllSources))]
        //public async Task<IActionResult> GetAllSources([FromQuery] GetAllSourceQuery query)
        //{
        //    return Ok(await mediator.Send(query));

        //}

        //[HttpGet("source/{id}", Name = nameof(GetSourceById))]
        //public async Task<IActionResult> GetSourceById([FromRoute] GetSourceByIdQuery query)
        //{
        //    return Ok(await mediator.Send(query));

        //}


        //[HttpPut("source", Name = nameof(UpdateSource))]
        //public async Task<IActionResult> UpdateSource([FromBody] UpdateSourceCommand command)
        //{
        //    await mediator.Send(command);
        //    return NoContent();

        //}
        //#endregion

        #region categories
      
        #endregion
    }
}
