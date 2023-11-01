using ServerStatisticsCollectionService.MessageSerializers.Interface;
using System.Text.Json;

namespace ServerStatisticsCollectionService.MessageSerializers
{
    public class JsonSerializerAdapter : IMessageSerializer
    {
        public string Serialize<T>(T obj)
        {
           return JsonSerializer.Serialize(obj);
        }
    }
}
