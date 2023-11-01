using MessageProcessingAndAnomalyDetectionService.AlertCalculators.Interface;
using MessageProcessingAndAnomalyDetectionService.Enums;
using MessageProcessingAndAnomalyDetectionService.Models;
using ServerStatisticsProcessingProcess.Models;

namespace MessageProcessingAndAnomalyDetectionService.AlertCalculators
{
    public class AlertCalculator : IAlertCalculator
    {
        private readonly AnomalyDetectionConfig _config;
        private double _prevMemoryUsage = -1;
        private double _prevCpuUsage = -1;
        public AlertCalculator(AnomalyDetectionConfig config) 
        {
            _config = config;
        }
        public IEnumerable<Alert> GetAlerts(ServerStatistics serverStatistics)
        {

            if (_prevMemoryUsage == -1 || _prevCpuUsage == -1)
            {
                _prevMemoryUsage = serverStatistics.MemoryUsage;
                _prevCpuUsage = serverStatistics.CpuUsage;
                return Enumerable.Empty<Alert>();
            }
                

            List<Alert> alerts = new List<Alert>();

            if (serverStatistics.MemoryUsage > (_prevMemoryUsage * (1 + _config.MemoryUsageAnomalyThresholdPercentage)))
            {
                alerts.Add(new Alert
                {
                    AlertType=AlertTypeEnum.HighUsageAlert,
                    Message= "Memory Usage Anomaly Alert"
                });
            }

            if (serverStatistics.CpuUsage > (_prevCpuUsage * (1 + _config.CpuUsageAnomalyThresholdPercentage)))
            {
                alerts.Add(new Alert
                {
                    AlertType = AlertTypeEnum.AnomalyAlert,
                    Message= "CPU Usage Anomaly Alert"
                });
            }
            
            if ((serverStatistics.MemoryUsage / (serverStatistics.MemoryUsage+ serverStatistics.AvailableMemory)) > _config.MemoryUsageThresholdPercentage)
            {
                alerts.Add(new Alert
                {
                    AlertType= AlertTypeEnum.HighUsageAlert,
                    Message = "Memory High Usage Alert"
                });
            }


            _prevMemoryUsage = serverStatistics.MemoryUsage;
            _prevCpuUsage = serverStatistics.CpuUsage;
            return alerts;
        }
    }
}
