namespace MessageProcessingAndAnomalyDetectionService.Models
{
    public class AnomalyDetectionConfig
    {
        public double MemoryUsageAnomalyThresholdPercentage { get; set; }
        public double CpuUsageAnomalyThresholdPercentage { get; set; }
        public double MemoryUsageThresholdPercentage { get; set; }
    }
}
