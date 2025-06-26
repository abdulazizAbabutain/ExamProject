using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Commons.Behaviors;

public class MetricsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<MetricsBehavior<TRequest, TResponse>> _logger;

    public MetricsBehavior(ILogger<MetricsBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var response = await next();
        stopwatch.Stop();

        var elapsed = stopwatch.ElapsedMilliseconds;
        var requestName = typeof(TRequest).Name;

        if (elapsed > 1000)
        {
            _logger.LogWarning("⚠️ {RequestName} took {Elapsed} ms", requestName, elapsed);
        }
        else
        {
            _logger.LogDebug("⏱ {RequestName} completed in {Elapsed} ms", requestName, elapsed);
        }

        return response;
    }
}
