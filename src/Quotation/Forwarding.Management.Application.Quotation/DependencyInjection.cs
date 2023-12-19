using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById;
using Forwarding.Management.Application.Quotation.Commands.DeleteQuotationRequestById.Contracts;
using Forwarding.Management.Application.Quotation.Commands.RequestQuotation;
using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById;
using Forwarding.Management.Application.Quotation.Commands.WithdrawQuotationRequestById.Contracts;
using Forwarding.Management.Application.Quotation.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests;
using Forwarding.Management.Application.Quotation.Queries.ListQuotationRequests.Contracts;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes;
using Forwarding.Management.Application.Quotation.Queries.ListQuotes.Contracts;
using Forwarding.Management.Domain.Quotation.Model;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Forwarding.Management.Application.Quotation;
public static class DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static void AddQuotationApplication(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IWriteStorageService<QuotationRequest>, RequestQuotationStorageService>();
        services.AddScoped<IDeleteQuotationRequestByIdService, DeleteQuotationRequestByIdStorageService>();
        services.AddScoped<IWithdrawQuotationRequestByIdStorageService, WithdrawQuotationRequestByIdStorageService>();
        services.AddScoped<IListQuotationRequestsStorageService, ListQuotationRequestsStorageService>();
        services.AddScoped<IListQuotesStorageService, ListQuotesStorageService>();

        var connectionString = configuration["CosmosDb:ConnectionString"];

        var options = new CosmosClientOptions
        {
            SerializerOptions = new CosmosSerializationOptions
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        };

        services.AddSingleton(_ => new CosmosClient(connectionString, options));

        if (environment.IsEnvironment("Local"))
        {
            services.AddHostedService<LocalDbInitializationService>();
        }
    }
}
