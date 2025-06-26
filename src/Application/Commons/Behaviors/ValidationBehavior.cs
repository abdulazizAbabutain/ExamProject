using FluentValidation;
using MediatR;

namespace Application.Commons.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = results.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Any())
            {
                var errorMessages = failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}").ToList();

                var resultType = typeof(TResponse);
                var failureMethod = resultType.GetMethod("Failure", new[] { typeof(IEnumerable<string>) });

                if (failureMethod != null)
                {
                    var failureResult = failureMethod.Invoke(null, new object[] { errorMessages });
                    return (TResponse)failureResult!;
                }
            }
        }

        return await next();
    }
}
