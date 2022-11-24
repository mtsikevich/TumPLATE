using Hangfire;
using Microsoft.AspNetCore.Builder;
using TumPLATE.Infrastructure.KafkaIntegration;

namespace TumPLATE.Application.Features.BackgroundJobs;

public static class KafkaBackgroundJobs
{
    public static void StartPollingTheKafkaTopic(this IApplicationBuilder app,string topic)
    {
        RecurringJob.AddOrUpdate<IKafkaIntegration>("Polling Kafka",ks => ks.SubscribeAsync(topic), "*/5 * * * * *");
    }
}