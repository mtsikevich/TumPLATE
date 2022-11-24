namespace TumPLATE.Infrastructure.KafkaIntegration;

public interface IKafkaIntegration
{
    Task<string> SubscribeAsync(string topicName);
    Task<bool> SendToTopicAsync(string topic, string jsonData);
}