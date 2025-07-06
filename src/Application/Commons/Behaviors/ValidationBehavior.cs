using FluentValidation;
using MediatR;
using System.Net;

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
                // Group by property name to match expected Dictionary<string, string[]> structure
                var errorDictionary = failures
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                var resultType = typeof(TResponse);
                var failureMethod = resultType.GetMethod("Failure", new[] { typeof(Dictionary<string, string[]>), typeof(HttpStatusCode) });

                if (failureMethod != null)
                {
                    var failureResult = failureMethod.Invoke(null, new object[] { errorDictionary, HttpStatusCode.BadRequest });
                    return (TResponse)failureResult!;
                }
            }
        }

        return await next();
    }
}
