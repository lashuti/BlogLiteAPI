using Amazon.SQS.Model;

namespace BlogLiteAPI;

public interface ISqsService
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken);
    Task<ReceiveMessageResponse> ReceiveAsync(int waitTimeSeconds, CancellationToken cancellationToken);
    Task AckMessageAsync(string receiptHandle, CancellationToken cancellationToken);
}