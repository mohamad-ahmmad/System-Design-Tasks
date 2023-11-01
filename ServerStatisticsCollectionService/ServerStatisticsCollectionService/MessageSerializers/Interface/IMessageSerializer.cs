namespace ServerStatisticsCollectionService.MessageSerializers.Interface
{
    public interface IMessageSerializer
    {
        public string Serialize<T>(T obj);
    }
}
