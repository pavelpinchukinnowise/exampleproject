using System.Diagnostics.CodeAnalysis;
using Forwarding.Management.Application.Common.MediatR.Pipelines;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forwarding.Management.Application.Common;
public static class DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static void AddApplicationCommon(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
    }
}
