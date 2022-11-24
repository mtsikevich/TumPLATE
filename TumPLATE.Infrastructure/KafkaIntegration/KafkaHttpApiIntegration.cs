using System.Security.AccessControl;

namespace TumPLATE.Infrastructure.KafkaIntegration;

public class KafkaHttpApiIntegration: IKafkaIntegration
{
    private HttpClient _client;
    public KafkaHttpApiIntegration(HttpClient client)
    {
        _client = client;
    }

    public Task<string> SubscribeAsync(string topicName)
    {
        Console.WriteLine($"checking topic {topicName} for messages");

        Task.Delay(TimeSpan.FromSeconds(5));

        return Task.FromResult("{}");
    }

    public Task<bool> SendToTopicAsync(string topic, string jsonData)
    {
        Console.WriteLine($"Sending: {jsonData}, to topic: {topic}");
        
        Task.Delay(TimeSpan.FromSeconds(5));
        
        return Task.FromResult(true);
    }
}