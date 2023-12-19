using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Forwarding.Management.Infrastructure.Common.Correlation;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate next;
    private readonly ICorrelationHttpHeaderService headerService;
    private readonly ILogger<CorrelationIdMiddleware> logger;

    public CorrelationIdMiddleware(
        RequestDelegate next,
        ICorrelationHttpHeaderService headerService,
        ILogger<CorrelationIdMiddleware> logger)
    {
        this.next = next;
        this.headerService = headerService;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using var scope = logger.BeginScope(
            "Ensuring {CorrelationIdHeader} HTTP header in the request: {CorrelationId}",
            Constants.Headers.CorrelationId,
            headerService.CorrelationId
        );

        context.Response.Headers.Add(Constants.Headers.CorrelationId, headerService.CorrelationId);

        await next(context);
    }
}
