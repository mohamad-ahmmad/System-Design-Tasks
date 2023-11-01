namespace ServerStatisticsCollectionService.MessagePublishers.Interfaces
{
    public interface IMessagePublisher
    {
        public void Publish(string message);
    }
}
