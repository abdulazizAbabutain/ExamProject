using API.Interfaces;
using Application.Commons.Models.Results;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Services
{
    public class HttpResultResponder : IHttpResultResponder
    {
        public IActionResult FromResult(HttpContext context, Result result)
        {
            if (!result.IsSuccess)
            {
                context.Items["Result"] = result;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => new NoContentResult(),
                HttpStatusCode.Created => new StatusCodeResult(StatusCodes.Status201Created),
                HttpStatusCode.OK or _ => new OkResult()
            };
        }

        public IActionResult FromResult<T>(HttpContext context, Result<T> result)
        {
            if (!result.IsSuccess)
            {
                context.Items["Result"] = result;
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new EmptyResult();
            }

            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => new NoContentResult(),
                HttpStatusCode.Created => new CreatedResult(context.Request.Path, result.Value),
                HttpStatusCode.OK or _ => new OkObjectResult(result.Value)
            };
        }
    }
}
