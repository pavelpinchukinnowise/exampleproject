using Microsoft.AspNetCore.Http;

namespace Forwarding.Management.Infrastructure.Common.Correlation;
public sealed class CorrelationHttpHeaderService : ICorrelationHttpHeaderService
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CorrelationHttpHeaderService(
        IHttpContextAccessor httpContextAccessor
    )
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string? CorrelationId
    {
        get
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                return null;
            }

            var headerValue = httpContext.Request.Headers[Constants.Headers.CorrelationId].FirstOrDefault();
            if (headerValue is not null)
            {
                return headerValue;
            }

            var headerKey = Constants.Headers.CorrelationId.ToLowerInvariant();
            var header = httpContext.Request.Headers
                .FirstOrDefault(e => e.Key.ToLowerInvariant() == headerKey);

            headerValue = header.Value.FirstOrDefault();

            var correlationId = Guid.NewGuid();
            if (headerValue is not null)
            {
                if (Guid.TryParse(headerValue, out correlationId))
                {
                    return correlationId.ToString();
                }
                else
                {
                    httpContext.Request.Headers.Remove(header);
                }
            }

            CorrelationId = correlationId.ToString();

            return correlationId.ToString();
        }

        set
        {
            httpContextAccessor.HttpContext?.Request.Headers.Add(Constants.Headers.CorrelationId, value);
        }
    }
}
