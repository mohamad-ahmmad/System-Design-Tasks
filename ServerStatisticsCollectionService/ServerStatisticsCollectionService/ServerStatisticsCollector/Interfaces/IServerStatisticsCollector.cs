using ServerStatisticsCollectionService.Models;

namespace ServerStatisticsCollectionService.ServerStatisticsCollector.Interfaces
{
    public interface IServerStatisticsCollector
    {
        public ServerStatistics GetServerStatistics();
    }
}
