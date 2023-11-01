namespace ServerStatisticsProcessingProcess.Deserializers.Interface
{
    public interface IDeserializer<T>
    {
        public T Deserialize(string str);
    }
}
