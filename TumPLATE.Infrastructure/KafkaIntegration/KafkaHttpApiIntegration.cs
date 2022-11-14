namespace TumPLATE.Infrastructure.KafkaIntegration;

public class KafkaHttpApiIntegration: IKafkaIntegration
{
    public Task<string> PollTopicAsync(string topicName)
    {
        return Task.FromResult("{}");
    }

    public Task<bool> SendToTopicAsync(string topic, dynamic data)
    {
        return Task.FromResult(true);
    }
}