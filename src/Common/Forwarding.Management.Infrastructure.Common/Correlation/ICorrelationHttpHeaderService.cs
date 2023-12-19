namespace Forwarding.Management.Infrastructure.Common.Correlation;

public interface ICorrelationHttpHeaderService
{
    string? CorrelationId { get; set; }
}