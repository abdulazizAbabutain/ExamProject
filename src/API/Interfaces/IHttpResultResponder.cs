using Application.Commons.Models.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IHttpResultResponder
    {
        IActionResult FromResult(HttpContext context, Result result);
        IActionResult FromResult<T>(HttpContext context, Result<T> result);
    }
}
