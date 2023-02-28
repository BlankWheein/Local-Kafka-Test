using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;



CreateHostBuilder(args).Build().Run();
static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, collection) =>
    {
        //collection.AddHostedService<KafkaProducerHostedService>();
        collection.AddHostedService<KafkaConsumerHostedService>();
    });

