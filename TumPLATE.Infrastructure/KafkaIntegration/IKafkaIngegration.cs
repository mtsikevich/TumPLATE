namespace TumPLATE.Infrastructure.KafkaIntegration;

public interface IKafkaIntegration
{
    Task<string> PollTopicAsync(string topicName);
    Task<bool> SendToTopicAsync(string topic, dynamic data);
}