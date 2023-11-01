using RabbitMQ.Client;
using ServerStatisticsCollectionService.MessagePublishers.Interfaces;
using System.Text;

namespace ServerStatisticsCollectionService.MessagePublishers
{
    public class RabbitMQMessagePublisher : IMessagePublisher
    {
        private readonly IModel channel;
        private readonly string topic;
        public RabbitMQMessagePublisher(string hostName, string serverIdentifier) 
        {
            if(hostName == null) throw new ArgumentNullException(nameof(hostName));
            if (serverIdentifier == null) throw new ArgumentNullException(nameof(serverIdentifier));

            topic = $"ServerStatistics.{serverIdentifier}";
            var factory = new ConnectionFactory {HostName = hostName };
            var cnn = factory.CreateConnection();
            channel = cnn.CreateModel();
            
            channel.ExchangeDeclare($"ServerStatistics", ExchangeType.Topic);
        }
        
        public void Publish(string message)
        {
            channel.BasicPublish(exchange:"ServerStatistics"
                , routingKey:topic
                , basicProperties: null
                , body:Encoding.UTF8.GetBytes(message));
        }
    }
}
