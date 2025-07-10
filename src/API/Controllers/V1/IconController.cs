using API.Interfaces;
using Application.Icons.Commands.UploadNewIcon;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commons.Models.Icons;
using Domain.Enums;
using Domain.Extentions;

namespace API.Controllers.V1
{
    [Route("api/icons")]
    public class IconController(IMediator mediator, IHttpResultResponder resultResponder) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IHttpResultResponder _resultResponder = resultResponder;

        [HttpPost("upload")]
        public async Task<IActionResult> UploadIcon([FromForm] UploadNewIconCommand command)
        {
            return _resultResponder.FromResult(HttpContext, await _mediator.Send(command));
        }

        [HttpGet()]
        public IActionResult GetAvailableIcons()
        {
            var predefinedDir = Path.Combine("wwwroot", "Icons", "predefined");
            var uploadedDir = Path.Combine("wwwroot", "Icons", "uploaded");

            var predefinedIcons = Directory.GetFiles(predefinedDir)
                .Select(file => new IconsModel
                {
                    Id = file.GetGuidFromFileName(),
                    Name = file.GetOriginalNameFromFile(),
                    Url = $"/icons/predefined/{Path.GetFileName(file)}",
                    Source = IconSource.Predefined
                });

            var uploadedIcons = Directory.GetFiles(uploadedDir)
                .Select(file => new IconsModel
                {
                    Id = file.GetGuidFromFileName(),
                    Name = file.GetOriginalNameFromFile(),
                    Url = $"/icons/uploaded/{Path.GetFileName(file)}",
                    Source = IconSource.Uploaded
                });

            var allIcons = predefinedIcons.Concat(uploadedIcons).ToList();

            return Ok(allIcons);
        }


    }
}
