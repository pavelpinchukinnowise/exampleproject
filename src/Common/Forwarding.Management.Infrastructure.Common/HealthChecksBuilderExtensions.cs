using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forwarding.Management.Infrastructure.Common;
public static class HealthChecksBuilderExtensions
{
    [ExcludeFromCodeCoverage]
    public static IHealthChecksBuilder AddCommonHealthChecks(this IHealthChecksBuilder healthChecksBuilder, IConfiguration configuration)
    {
        var connectionString = configuration["CosmosDb:ConnectionString"];

        return healthChecksBuilder
            .AddCosmosDb(connectionString!);
    }
}
