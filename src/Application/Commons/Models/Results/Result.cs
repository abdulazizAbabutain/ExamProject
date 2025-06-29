using System.Net;

namespace Application.Commons.Models.Results;

public class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public List<string> Errors { get; init; } = new();
    public HttpStatusCode StatusCode { get; set; } 

    public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value, StatusCode = HttpStatusCode.OK };
    public static Result<T> Success(T value, HttpStatusCode statusCode) => new() { IsSuccess = true, Value = value, StatusCode = statusCode };

    public static Result<T> Failure(IEnumerable<string> errors) => new() { IsSuccess = false, Errors = errors.ToList() };
    public static Result<T> Failure(IEnumerable<string> errors, HttpStatusCode httpStatusCode) => new() { IsSuccess = false, Errors = errors.ToList(), StatusCode = httpStatusCode };
    public static Result<T> Failure(string error, HttpStatusCode httpStatusCode) => new() { IsSuccess = false, Errors = new() { error }, StatusCode = httpStatusCode };
    public static Result<T> Failure(string error)
    {
        var result = new Result<T>();
        result.Errors.Add(error);
        return result;
    }
    public Result<T> AddError(string error)
    {
        Errors.Add(error);
        return this;
    }


    public static Result<T> NotFoundFailure(string error) =>
      Failure(error, HttpStatusCode.NotFound);

    public static Result<T> ConflictFailure(string error) =>
        Failure(error, HttpStatusCode.Conflict);

    public static Result<T> UnprocessableEntityFailure(string error) =>
        Failure(error, HttpStatusCode.UnprocessableEntity);

    public static Result<T> CreatedSuccess(T value) =>
       Success(value, HttpStatusCode.Created);
}


public class Result
{
    public bool IsSuccess { get; init; }
    public List<string> Errors { get; init; } = new();
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

    public static Result Success() => new() { IsSuccess = true, StatusCode = HttpStatusCode.OK };
    public static Result Success(HttpStatusCode statusCode) => new() { IsSuccess = true, StatusCode = statusCode };

    public static Result Failure(IEnumerable<string> errors) =>
        new() { IsSuccess = false, Errors = errors.ToList(), StatusCode = HttpStatusCode.BadRequest };

    public static Result Failure(IEnumerable<string> errors, HttpStatusCode statusCode) =>
        new() { IsSuccess = false, Errors = errors.ToList(), StatusCode = statusCode };

    public static Result Failure(string error, HttpStatusCode statusCode) =>
        new() { IsSuccess = false, Errors = new List<string> { error }, StatusCode = statusCode };

    public static Result Failure(string error) =>
        new() { IsSuccess = false, Errors = new List<string> { error }, StatusCode = HttpStatusCode.BadRequest };

    public static Result NotFoundFailure(string error) =>
        Failure(error, HttpStatusCode.NotFound);

    public static Result ConflictFailure(string error) =>
        Failure(error, HttpStatusCode.Conflict);

    public static Result UnprocessableEntityFailure(string error) =>
        Failure(error, HttpStatusCode.UnprocessableEntity);

    public static Result NoContentSuccess() =>
       Success(HttpStatusCode.NoContent);
}
