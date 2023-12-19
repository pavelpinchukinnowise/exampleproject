using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Forwarding.Management.Infrastructure.Common.Correlation;

public sealed class CorrelationTelemetryInitializer : ITelemetryInitializer
{
    private readonly ICorrelationHttpHeaderService headerService;

    public CorrelationTelemetryInitializer(ICorrelationHttpHeaderService headerService)
    {
        this.headerService = headerService;
    }

    public void Initialize(ITelemetry telemetry)
    {
        telemetry.Context.GlobalProperties.TryAdd(
            Constants.Headers.CorrelationId, 
            headerService.CorrelationId ?? telemetry.Context.Operation.Id);
    }
}
