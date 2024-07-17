using BlogLiteAPI;

namespace BlogLite.Worker
{
    public class QueueReader(ISqsService sqs) : BackgroundService
    {
        private readonly ISqsService _sqs = sqs;

        private const int WAIT_TIME_SECONDS = 10;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = (await _sqs.ReceiveAsync(WAIT_TIME_SECONDS, stoppingToken)).Messages.FirstOrDefault();

                if (message != null)
                {
                    //process
                    //send to mail and sms

                    await _sqs.AckMessageAsync(message.ReceiptHandle, stoppingToken);
                }
            }
        }
    }
}
