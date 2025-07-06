using Application.Commons.Models.Results;
using Newtonsoft.Json;
using System.Net;

namespace API.Middleware;
public class ResultMiddleware
{
    private readonly RequestDelegate _next;

    public ResultMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.HasStarted || context.Response.StatusCode < 400)
            return;

        if (context.Items.TryGetValue("Result", out var resultObj) && resultObj is not null)
        {
            HttpStatusCode statusCode;
            Dictionary<string, string[]> errors;
            bool isSuccess;

            if (resultObj is Result result)
            {
                statusCode = result.StatusCode;
                errors = result.Errors;
                isSuccess = result.IsSuccess;
            }
            else if (resultObj.GetType().IsGenericType &&
                     resultObj.GetType().GetGenericTypeDefinition() == typeof(Result<>))
            {
                var type = resultObj.GetType();
                isSuccess = (bool)type.GetProperty("IsSuccess")?.GetValue(resultObj)!;
                statusCode = (HttpStatusCode)type.GetProperty("StatusCode")?.GetValue(resultObj)!;
                errors = (Dictionary<string, string[]>)type.GetProperty("Errors")?.GetValue(resultObj)!;
            }
            else return;

            if (isSuccess) return;

            var problemDetails = new
            {
                type = GetProblemType(statusCode),
                title = GetTitleForStatus(statusCode),
                status = (int)statusCode,
                detail =  errors,
                instance = context.Request.Path
            };

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/problem+json";

            var json = JsonConvert.SerializeObject(problemDetails);
            await context.Response.WriteAsync(json);
        }

    }

    private static string GetProblemType(HttpStatusCode code) =>
        code switch
        {
            HttpStatusCode.NotFound => "https://httpstatuses.com/404",
            HttpStatusCode.Conflict => "https://httpstatuses.com/409",
            HttpStatusCode.UnprocessableEntity => "https://httpstatuses.com/422",
            HttpStatusCode.BadRequest => "https://httpstatuses.com/400",
            HttpStatusCode.Unauthorized => "https://httpstatuses.com/401",
            HttpStatusCode.Forbidden => "https://httpstatuses.com/403",
            _ => $"https://httpstatuses.com/{(int)code}"
        };

    private static string GetTitleForStatus(HttpStatusCode code) =>
        code switch
        {
            HttpStatusCode.NotFound => "Not Found",
            HttpStatusCode.Conflict => "Conflict",
            HttpStatusCode.UnprocessableEntity => "Unprocessable Entity",
            HttpStatusCode.BadRequest => "Bad Request",
            HttpStatusCode.Unauthorized => "Unauthorized",
            HttpStatusCode.Forbidden => "Forbidden",
            _ => "Error"
        };
}



