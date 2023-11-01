using MessageProcessingAndAnomalyDetectionService.Models;
using ServerStatisticsProcessingProcess.Models;

namespace MessageProcessingAndAnomalyDetectionService.AlertCalculators.Interface
{
    public interface IAlertCalculator
    {
        public IEnumerable<Alert> GetAlerts
            (ServerStatistics serverStatistics);
    }
}
