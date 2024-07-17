
using System.Text.Json;
using System.Threading;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace BlogLiteAPI;

public class SqsService() : ISqsService
{
    public IAmazonSQS _sqs = new AmazonSQSClient(RegionEndpoint.EUCentral1);
    private readonly string _queueName = "BlogLiteQueue";
    private string? _queueUrl;

    private async Task<string> GetQueueUrlAsync(CancellationToken cancellationToken)
    {
        if (_queueUrl == null)
        {
            var response = await _sqs.GetQueueUrlAsync(_queueName, cancellationToken);
            _queueUrl = response.QueueUrl;
        }

        return _queueUrl;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken)
    {
        var queueUrl = await GetQueueUrlAsync(cancellationToken);
        var request = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = JsonSerializer.Serialize(message)
        };

        await _sqs.SendMessageAsync(request, cancellationToken);
    }

    public async Task<ReceiveMessageResponse> ReceiveAsync(int waitTimeSeconds, CancellationToken cancellationToken)
    {
        var queueUrl = await GetQueueUrlAsync(cancellationToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            WaitTimeSeconds = waitTimeSeconds,
            MaxNumberOfMessages = 1
        };

        return await _sqs.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);
    }

    public async Task AckMessageAsync(string receiptHandle, CancellationToken cancellationToken)
    {
        var queueUrl = await GetQueueUrlAsync(cancellationToken);
        
        await _sqs.DeleteMessageAsync(queueUrl, receiptHandle, cancellationToken);
    }
}
