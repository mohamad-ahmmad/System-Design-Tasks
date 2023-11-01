using MessageProcessingAndAnomalyDetectionService.Models;

namespace MessageProcessingAndAnomalyDetectionService.ServerStatisticsHub.Interface
{
    public interface IServerStatisticsHub
    {
        public Task SendAlertsToClientsAsync(IEnumerable<Alert> alerts);
    }
}
