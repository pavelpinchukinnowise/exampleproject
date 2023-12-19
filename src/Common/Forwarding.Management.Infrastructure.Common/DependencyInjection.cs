using Forwarding.Management.Infrastructure.Common.Correlation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Forwarding.Management.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCorrelationLogging(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddHeaderPropagation(options => options.Headers.Add(Constants.Headers.CorrelationId));
        services.AddTransient<ITelemetryInitializer, CorrelationTelemetryInitializer>();
        services.AddTransient<ICorrelationHttpHeaderService, CorrelationHttpHeaderService>();

        return services;
    }

    public static IApplicationBuilder UseCorrelationHeader(this IApplicationBuilder builder) =>
        builder.UseMiddleware<CorrelationIdMiddleware>();
}
