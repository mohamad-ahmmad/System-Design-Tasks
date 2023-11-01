using ServerStatisticsProcessingProcess.Models;

namespace MessageProcessingAndAnomalyDetectionService.Repositories.Interface
{
    public interface IServerStatisticsRepository
    {
        public void Add(ServerStatistics serverStatistics);
    }
}
