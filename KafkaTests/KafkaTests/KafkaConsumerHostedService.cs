using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;

public class KafkaConsumerHostedService : IHostedService
{

    private readonly ILogger<KafkaConsumerHostedService> _logger;
    private IConsumer<Null, byte[]> _consumer;
    private string topic = "topic1";

    public KafkaConsumerHostedService(ILogger<KafkaConsumerHostedService> logger)
    {

        var config = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9092",
            GroupId = "9",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Null, byte[]>(config).Build();
        _logger = logger;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(topic);
        _logger.LogInformation("Started consumer");
        await Task.Factory.StartNew(() =>
        {
            while (true)
            {
                try
                {
                    ConsumeResult<Null, byte[]> res = _consumer.Consume(cancellationToken);
                    if (res != null) {
                        EventRaisedMessage? val = ProtoSerializer.GPBDeserialization(res.Message.Value) as EventRaisedMessage;
                        if (val != null)
                            OnMessageConsumedEvent(res.Message.Value, val);
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
            }
        }, cancellationToken);
    }
    private void OnMessageConsumedEvent(byte[] res, EventRaisedMessage message)
    {
        _logger.LogInformation($"Consumed message: {ByteArrayToString(res)} - {message.Date}");
    }

    public string ByteArrayToString(byte[] data)
    {
        var @string = "'";
        data.ToList().ForEach(p =>
        {
            @string += p.ToString() + " ";
        });
        return @string[0..^1] + "'";
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer?.Dispose();
        return Task.CompletedTask;
    }
}
