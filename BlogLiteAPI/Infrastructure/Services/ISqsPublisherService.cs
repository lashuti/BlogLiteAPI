namespace BlogLiteAPI;

public interface ISqsPublisherService
{
    Task PublishAsync<T>(T message);
}