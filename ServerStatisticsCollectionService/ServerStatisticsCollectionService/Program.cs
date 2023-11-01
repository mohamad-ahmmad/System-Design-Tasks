using Microsoft.Extensions.Configuration;
using ServerStatisticsCollectionService;
using ServerStatisticsCollectionService.MessagePublishers;
using ServerStatisticsCollectionService.MessageSerializers;
using ServerStatisticsCollectionService.ServerStatisticsCollector;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var secondsDelay = int.Parse(configuration.GetSection("ServerStatisticsConfig:SamplingIntervalSeconds").Value);
var serverIdentifier = configuration.GetSection("ServerStatisticsConfig:ServerIdentifier").Value;
var messageQueueHost = configuration.GetSection("AppSettings:MessageBrokerHost").Value;

new ServerStatisticsPublisher(new RabbitMQMessagePublisher(messageQueueHost!, serverIdentifier!),
                            new ServerStatisticsCollector(secondsDelay),
                            new JsonSerializerAdapter())
                            .Run();