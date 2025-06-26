namespace Application.Commons.Models.Results;

public class Result<T>
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public List<string> Errors { get; init; } = new();

    public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };
    public static Result<T> Failure(IEnumerable<string> errors) => new() { IsSuccess = false, Errors = errors.ToList() };
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
}
