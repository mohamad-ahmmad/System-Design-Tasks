using MessageProcessingAndAnomalyDetectionService;
using MessageProcessingAndAnomalyDetectionService.AlertCalculators;
using MessageProcessingAndAnomalyDetectionService.Models;
using MessageProcessingAndAnomalyDetectionService.Repositories;
using MessageProcessingAndAnomalyDetectionService.ServerStatisticsHub;
using Microsoft.Extensions.Configuration;

using ServerStatisticsProcessingProcess.Deserializers;
using ServerStatisticsProcessingProcess.MessageSubscriber;

try
{
    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

    var hostName = configuration["RabbitMQConfig:Host"] ?? throw new ArgumentNullException("RabbitMQConfig:Host");
    var rabbitMQExchange = configuration["RabbitMQConfig:Exchange"] ?? throw new ArgumentNullException("RabbitMQConfig:Exchange");
    var mongoDbConnectionString = configuration["MongoDBConfig:ConnectionString"] ?? throw new ArgumentNullException("MongoDBConfig:ConnectionString");
    var signalrUrl = configuration["SignalRConfig:SignalRUrl"] ?? throw new ArgumentNullException("SignalRConfig:SignalRUrl");
    var anomalyConfigs = new AnomalyDetectionConfig();
    configuration.GetSection("AnomalyDetectionConfig").Bind(anomalyConfigs);


    new MessageAndAnomalyDetectionService(new JsonDeserialzerAdapter(),
        new RabbitMQMessageConsumer(hostName, rabbitMQExchange),
        new DocumentServerStatisticsRepository(mongoDbConnectionString),
        new ServerStatisticsHub(signalrUrl),
        new AlertCalculator(anomalyConfigs))
        .Run(rabbitMQExchange+".*");

    Console.WriteLine("Press any key to exit.");
    Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}


