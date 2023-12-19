using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Azure;
using Azure.Maps.Routing;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forwarding.Management.Application.Locations;
public static class DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static void AddLocationsApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton(_ =>
        {
            var subscriptionKey = configuration["AzureMaps:AuthOptions:SubscriptionKey"];

            if (string.IsNullOrWhiteSpace(subscriptionKey))
            {
                throw new InvalidOperationException("Azure Maps subscription key is missing or empty.");
            }

            var credential = new AzureKeyCredential(subscriptionKey);
            var client = new MapsRoutingClient(credential);

            return client;
        });
    }
}