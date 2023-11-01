using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServerStatisticsProcessingProcess.MessageSubscriber.Interface;

namespace ServerStatisticsProcessingProcess.MessageSubscriber
{
    public class RabbitMQMessageConsumer : IMessageConsumer<BasicDeliverEventArgs>
    {
        private EventingBasicConsumer _consumer;
        private string _queueName;
        private IModel _channel;
        public RabbitMQMessageConsumer(string hostName, string exchange)
        {
            if (hostName == null) throw new ArgumentNullException(nameof(hostName));
            if (exchange == null) throw new ArgumentNullException(nameof(exchange));

            SetupConnection(hostName, exchange);
        }
        private void SetupConnection(string hostName, string exchange)
        {
            var factory = new ConnectionFactory { HostName = hostName };
            var cnn = factory.CreateConnection();
            _channel = cnn.CreateModel();

            _channel.ExchangeDeclare(exchange, type: ExchangeType.Topic);
            _queueName = _channel.QueueDeclare().QueueName;

            _consumer = new EventingBasicConsumer(_channel);
            _channel.BasicConsume(queue: _queueName,
                     autoAck: true,
                     consumer: _consumer);

        }
        public void AddMessageHandler(EventHandler<BasicDeliverEventArgs> handler, string routingKeyWildcard)
        {
            _channel.QueueBind(queue: _queueName,
                       exchange: "ServerStatistics",
                       routingKey: routingKeyWildcard
                       );

            _consumer.Received += handler;
        }

        public void RemoveMessageHandler(EventHandler<BasicDeliverEventArgs> handler, string routingKeyWildcard)
        {
            throw new NotImplementedException();
        }
    }
}
