using FastEndpoints.Swagger;
using Microsoft.Identity.Web;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace Forwarding.Management.Api.Extensions;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = new MicrosoftIdentityOptions();
        configuration.Bind("Auth:ApplicationRegistration", identityOptions);

        services.SwaggerDocument(options =>
        {
            options.MaxEndpointVersion = 1;
            options.ShortSchemaNames = true;
            options.EnableJWTBearerAuth = false;
            options.DocumentSettings = settings => {
                settings.Title = "Forwarding Management API";
                settings.Version = "v1.0";
                settings.DocumentName = "v1";

                settings.AddSecurity("Azure AD B2C", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "Azure AD B2C Authentication",
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = $"{identityOptions.Instance}/{identityOptions.Domain}/{identityOptions.SignUpSignInPolicyId}/oauth2/v2.0/authorize",
                            TokenUrl = $"{identityOptions.Instance}/{identityOptions.Domain}/{identityOptions.SignUpSignInPolicyId}/oauth2/v2.0/token",
                            Scopes = configuration
                                .GetSection("Auth:ApplicationRegistration:Scopes")
                                .GetChildren()
                                .ToDictionary(x => x.Value!, x => x.Key)
                        }
                    }
                });

                settings.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Azure AD B2C"));
            };
        });
        return services;
    }
}