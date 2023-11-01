namespace ServerStatisticsProcessingProcess.MessageSubscriber.Interface
{
    public interface IMessageConsumer<T>
    {
        public void AddMessageHandler(EventHandler<T> handler, string routingKeyWildcard);
        public void RemoveMessageHandler(EventHandler<T> handler, string routingKeyWildcard);
    }
}
