using System.Text.Json.Serialization;
using FastEndpoints;
using Forwarding.Management.Api.Constants;
using Forwarding.Management.Api.Extensions;
using Forwarding.Management.Api.Middlewares;
using Forwarding.Management.Application.Common;
using Forwarding.Management.Application.Invoicing;
using Forwarding.Management.Application.Locations;
using Forwarding.Management.Application.Operation;
using Forwarding.Management.Application.Quotation;
using Forwarding.Management.Infrastructure.Common;
using Forwarding.Management.Infrastructure.Invoicing;
using Forwarding.Management.Infrastructure.Locations;
using Forwarding.Management.Infrastructure.Operation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NSwag.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services.AddCorrelationLogging();
services.AddApplicationInsightsTelemetry();

services.AddFastEndpoints();

services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

services.AddSwaggerConfiguration(configuration);

services.AddCors(options =>
{
    options.AddPolicy("AllowAnyCorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(HttpConstants.ContinuationTokenHeader);
    });
});

var applicationRegistrationConfig = builder.Configuration.GetSection("Auth").GetSection("ApplicationRegistration");
var applicationSecurityConfig = builder.Configuration.GetSection("Auth").GetSection("ApplicationSecurity");

services
    .AddHealthChecks()
    .AddCommonHealthChecks(configuration);

services.AddApplicationCommon(configuration);

services.AddOperationApplication(configuration);
services.AddOperationInfrastructure(configuration);

services.AddQuotationApplication(configuration, builder.Environment);

services.AddInvoicingApplication(configuration);
services.AddInvoicingInfrastructure(configuration);

services.AddLocationsInfrastructure(configuration);
services.AddLocationsApplication(configuration);

services.AddHttpClient().AddHeaderPropagation();

var app = builder.Build();

app.UseCorrelationHeader();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Endpoints.ShortNames = true;
});

app.UseCors("AllowAnyCorsPolicy");

if (!app.Environment.IsProduction() && !app.Environment.IsStaging())
{
    app.UseOpenApi();
    app.UseSwaggerUi3(settings =>
    {
        var clientSettings = new OAuth2ClientSettings
        {
            ClientId = configuration["Auth:ApplicationRegistration:SwaggerClientId"],
            AppName = "Forwarding Management"
        };

        var scopes = builder.Configuration
                            .GetSection("Auth:ApplicationRegistration:Scopes")
                            .GetChildren()
                            .Select(x => x.Value);

        foreach (var scope in scopes)
        {
            clientSettings.Scopes.Add(scope);
        }

        settings.AdditionalSettings.Add("displayRequestDuration", true); 
        settings.OAuth2Client = clientSettings;
    });
}

app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});

await app.RunAsync();

public partial class Program { }
