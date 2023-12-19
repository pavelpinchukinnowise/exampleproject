using System.Diagnostics.CodeAnalysis;
using Azure;
using Azure.Maps.Search;
using Forwarding.Management.Application.Locations.Contracts;
using Forwarding.Management.Application.Locations.Queries.Ports.GetPortsByFilters;
using Forwarding.Management.Domain.Location.Contracts;
using Forwarding.Management.Infrastructure.Locations.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forwarding.Management.Infrastructure.Locations;

public static class DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static void AddLocationsInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IGeoLocationService, GeoLocationService>();
        services.AddScoped<IPortsService, PortsService>();

        services.AddSingleton(_ =>
        {
            var credential =
                new AzureKeyCredential(configuration["AzureMaps:AuthOptions:SubscriptionKey"]!);
            var client = new MapsSearchClient(credential);

            return client;
        });
    }
}