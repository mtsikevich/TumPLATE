using Hangfire;
using Microsoft.AspNetCore.Builder;
using TumPLATE.Infrastructure.KafkaIntegration;

namespace TumPLATE.Application.Features.BackgroundJobs;

public static class KafkaBackgroundJobs
{
    public static void StartPollingTheKafkaTopic(this IApplicationBuilder app,string topic, string jobName)
    {
        RecurringJob.AddOrUpdate<IKafkaIntegration>(jobName,ks => ks.SubscribeAsync(topic), "*/5 * * * * *");
    }
}