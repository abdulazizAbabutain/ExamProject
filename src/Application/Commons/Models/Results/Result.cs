using System.Collections.Generic;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Commons.Models.Results;

public class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public Dictionary<string, string[]> Errors { get; init; } = new();
    public HttpStatusCode StatusCode { get; set; }

    public static Result<T> Success(T value) => new()
    {
        IsSuccess = true,
        Value = value,
        StatusCode = HttpStatusCode.OK
    };

    public static Result<T> Success(T value, HttpStatusCode statusCode) => new()
    {
        IsSuccess = true,
        Value = value,
        StatusCode = statusCode
    };

    public static Result<T> Failure(Dictionary<string, string[]> errors) => new()
    {
        IsSuccess = false,
        Errors = errors
    };

    public static Result<T> Failure(Dictionary<string, string[]> errors, HttpStatusCode statusCode) => new()
    {
        IsSuccess = false,
        Errors = errors,
        StatusCode = statusCode
    };

    public static Result<T> Failure(string key, string error) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, new[] { error } } }
    };

    public static Result<T> Failure(string key, string error, HttpStatusCode statusCode) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, new[] { error } } },
        StatusCode = statusCode
    };

    public static Result<T> Failure(string key, IEnumerable<string> errors) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, errors.ToArray() } }
    };

    public static Result<T> Failure(string key, IEnumerable<string> errors, HttpStatusCode statusCode) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, errors.ToArray() } },
        StatusCode = statusCode
    };

    public Result<T> AddError(string key, string error)
    {
        if (!Errors.ContainsKey(key))
            Errors[key] = new[] { error };
        else
            Errors[key] = Errors[key].Append(error).ToArray();

        return this;
    }

    public static Result<T> NotFoundFailure(string key, string error) =>
        Failure(key, error, HttpStatusCode.NotFound);

    public static Result<T> ConflictFailure(string key, string error) =>
        Failure(key, error, HttpStatusCode.Conflict);

    public static Result<T> UnprocessableEntityFailure(string key, string error) =>
        Failure(key, error, HttpStatusCode.UnprocessableEntity);

    public static Result<T> UnprocessableEntityFailure(string key, IEnumerable<string> errors) =>
        Failure(key, errors, HttpStatusCode.UnprocessableEntity);

    public static Result<T> UnprocessableEntityFailure(Dictionary<string, string[]> errors) =>
    Failure(errors, HttpStatusCode.UnprocessableEntity);

    public static Result<T> CreatedSuccess(T value) =>
        Success(value, HttpStatusCode.Created);
}

public class Result
{
    public bool IsSuccess { get; init; }
    public Dictionary<string, string[]> Errors { get; init; } = new();
    public HttpStatusCode StatusCode { get; set; }

    public static Result Success() => new()
    {
        IsSuccess = true,
        StatusCode = HttpStatusCode.OK
    };

    public static Result Success(HttpStatusCode statusCode) => new()
    {
        IsSuccess = true,
        StatusCode = statusCode
    };

    public static Result Failure(Dictionary<string, string[]> errors) => new()
    {
        IsSuccess = false,
        Errors = errors
    };

    public static Result Failure(Dictionary<string, string[]> errors, HttpStatusCode statusCode) => new()
    {
        IsSuccess = false,
        Errors = errors,
        StatusCode = statusCode
    };

    public static Result Failure(string key, string error) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, new[] { error } } }
    };

    public static Result Failure(string key, string error, HttpStatusCode statusCode) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, new[] { error } } },
        StatusCode = statusCode
    };

    public static Result Failure(string key, IEnumerable<string> errors) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, errors.ToArray() } }
    };

    public static Result Failure(string key, IEnumerable<string> errors, HttpStatusCode statusCode) => new()
    {
        IsSuccess = false,
        Errors = new Dictionary<string, string[]> { { key, errors.ToArray() } },
        StatusCode = statusCode
    };

    public Result AddError(string key, string error)
    {
        if (!Errors.ContainsKey(key))
            Errors[key] = new[] { error };
        else
            Errors[key] = Errors[key].Append(error).ToArray();

        return this;
    }

    public static Result NotFoundFailure(string key, string error) =>
        Failure(key, error, HttpStatusCode.NotFound);

    public static Result ConflictFailure(string key, string error) =>
        Failure(key, error, HttpStatusCode.Conflict);

    public static Result UnprocessableEntityFailure(string key, string error) =>
        Failure(key, error, HttpStatusCode.UnprocessableEntity);

    public static Result UnprocessableEntityFailure(string key, IEnumerable<string> errors) =>
        Failure(key, errors, HttpStatusCode.UnprocessableEntity);

    public static Result NoContentSuccess()
        => Success(HttpStatusCode.NoContent); 
}
