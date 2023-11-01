using ServerStatisticsCollectionService.MessagePublishers.Interfaces;

namespace ServerStatisticsCollectionService.MessagePublishers
{
    public class ConsoleMessagePublisher : IMessagePublisher
    {
        public void Publish(string message)
        {
            Console.WriteLine(message);
        }
    }
}
