using System.Diagnostics;
using OpenTelemetry.Resources;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

namespace TumPLATE.Infrastructure.Observability;

public static class OpenTelemetryObservabilityExtensions
{ 
    
    public static void AddObservability(this IServiceCollection services, string serviceName, string serviceVersion)
    {
        services.AddOpenTelemetryTracing(builder =>
        {
            builder
                .AddConsoleExporter()
                .AddJaegerExporter()
                .AddSource(serviceName)
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(serviceName, serviceVersion: serviceVersion))
                .AddAspNetCoreInstrumentation()
                
                .AddHttpClientInstrumentation();
        });

        services.AddSingleton<ActivitySource>(new ActivitySource(serviceName));
    }
}