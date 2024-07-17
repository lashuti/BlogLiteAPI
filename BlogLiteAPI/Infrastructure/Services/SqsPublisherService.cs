
using System.Text.Json;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace BlogLiteAPI;

public class SqsPublisherService() : ISqsPublisherService
{
    public IAmazonSQS _sqs = new AmazonSQSClient(RegionEndpoint.EUCentral1);
    private readonly string queueName = "BlogLiteQueue";
    
    public async Task PublishAsync<T>(T message)
    {
        var queueUrl = await _sqs.GetQueueUrlAsync(queueName);
        var request = new SendMessageRequest
        {
            QueueUrl = queueUrl.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message)
        };

        await _sqs.SendMessageAsync(request);
    }
}
