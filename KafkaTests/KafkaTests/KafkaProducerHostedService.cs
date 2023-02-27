using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class KafkaProducerHostedService : IHostedService
{
    private readonly ILogger<KafkaProducerHostedService> _logger;
    private IProducer<Null, byte[]> _producer;
    private string topic = "topic5";

    public KafkaProducerHostedService(ILogger<KafkaProducerHostedService> logger)
    {
        _logger = logger;
        var config = new ProducerConfig()
        {
            BootstrapServers = "localhost:9092"
        };
        _producer = new ProducerBuilder<Null, byte[]>(config).Build();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        int i = 0;
        _logger.LogInformation("Started Producer");
        await Task.Factory.StartNew(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                i++;
                var value = new EventRaisedMessage(DateTime.Now);
                _logger.LogInformation("Produced: {}", value.ToString());
                await _producer.ProduceAsync(topic, new Message<Null, byte[]>()
                {
                    Value = ProtoSerializer.GPBSerialization(value)
                }, cancellationToken);
                await Task.Delay(1000, cancellationToken);
            }
            _producer.Flush(timeout: TimeSpan.FromSeconds(10));
        });
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _producer?.Dispose();
        return Task.CompletedTask;
    }
}
