namespace API.Middelware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdHeader = "X-Request-ID";

        public async Task Invoke(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var requestId))
                requestId = Guid.NewGuid().ToString();

            context.Items["RequestId"] = requestId;
            context.Response.Headers[CorrelationIdHeader] = requestId;

            await next(context);
        }
    }
}
