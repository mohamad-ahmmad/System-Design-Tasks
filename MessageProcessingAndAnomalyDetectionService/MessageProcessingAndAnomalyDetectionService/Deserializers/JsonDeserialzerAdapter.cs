using ServerStatisticsProcessingProcess.Deserializers.Interface;
using ServerStatisticsProcessingProcess.Models;
using System.Text.Json;

namespace ServerStatisticsProcessingProcess.Deserializers
{
    public class JsonDeserialzerAdapter : IDeserializer<ServerStatistics>
    {
        public ServerStatistics Deserialize(string str)
        {
            return JsonSerializer.Deserialize<ServerStatistics>(str);
        }
    }
}
