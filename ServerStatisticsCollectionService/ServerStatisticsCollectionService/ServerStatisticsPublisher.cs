using ServerStatisticsCollectionService.MessagePublishers.Interfaces;
using ServerStatisticsCollectionService.MessageSerializers.Interface;
using ServerStatisticsCollectionService.ServerStatisticsCollector.Interfaces;

namespace ServerStatisticsCollectionService
{
    public class ServerStatisticsPublisher
    {
        private readonly IMessagePublisher _publisher;
        private readonly IServerStatisticsCollector _serverStatisticsCollector;
        private readonly IMessageSerializer _serializer;
        public ServerStatisticsPublisher(IMessagePublisher publisher,
                                        IServerStatisticsCollector serverStatisticsCollector,
                                        IMessageSerializer messageSerializer) 
        {
            _publisher = publisher;
            _serverStatisticsCollector = serverStatisticsCollector;
            _serializer = messageSerializer;
        }

        public void Run()
        {
            while (true)
            {
                var statis = _serverStatisticsCollector.GetServerStatistics();
                var message = _serializer.Serialize(statis);
                _publisher.Publish(message);
            }
        }
    }
}
