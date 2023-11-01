using MessageProcessingAndAnomalyDetectionService.AlertCalculators.Interface;
using MessageProcessingAndAnomalyDetectionService.Repositories.Interface;
using MessageProcessingAndAnomalyDetectionService.ServerStatisticsHub.Interface;
using RabbitMQ.Client.Events;
using ServerStatisticsProcessingProcess.Deserializers.Interface;
using ServerStatisticsProcessingProcess.MessageSubscriber.Interface;
using ServerStatisticsProcessingProcess.Models;
using System.Text;

namespace MessageProcessingAndAnomalyDetectionService
{
    public class MessageAndAnomalyDetectionService
    {
        private readonly IDeserializer<ServerStatistics> _deserializer;
        private readonly IMessageConsumer<BasicDeliverEventArgs> _messageConsumer;
        private readonly IServerStatisticsRepository _serverStatisticsRepository;
        private readonly IServerStatisticsHub _serverStatisticsHub;
        private readonly IAlertCalculator _alertCalculator;

        public MessageAndAnomalyDetectionService
            (
            IDeserializer<ServerStatistics> deserializer,
            IMessageConsumer<BasicDeliverEventArgs> messageConsumer,
            IServerStatisticsRepository serverStatisticsRepository,
            IServerStatisticsHub serverStatisticsHub,
            IAlertCalculator alertCalculator) 
        {
            _deserializer = deserializer;
            _messageConsumer = messageConsumer;
            _serverStatisticsRepository= serverStatisticsRepository;
            _serverStatisticsHub = serverStatisticsHub;
            _alertCalculator = alertCalculator;
        }

        public void Run(string routingKey)
        {
            
            _messageConsumer.AddMessageHandler
                ((sender, args) =>
                {
                    string msg = Encoding.UTF8.GetString(args.Body.ToArray());
                    var stats = _deserializer.Deserialize(msg);
                    stats.ServerIdentifier = args.RoutingKey;
                    _serverStatisticsRepository.Add(stats);
                    var alerts = _alertCalculator.GetAlerts(stats);
                    _serverStatisticsHub.SendAlertsToClientsAsync(alerts);
                },
                routingKey);
        }
    }
}
