using Microsoft.Extensions.DependencyInjection;

namespace TumPLATE.Infrastructure.KafkaIntegration;

public static class KafkaIntegrationExtensions
{
    public static void AddKafkaIntegration(this IServiceCollection services)
    {
        services.AddSingleton<IKafkaIntegration, KafkaHttpApiIntegration>();
    }
}