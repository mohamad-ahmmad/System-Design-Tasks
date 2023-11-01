using MessageProcessingAndAnomalyDetectionService.Enums;

namespace MessageProcessingAndAnomalyDetectionService.Models
{
    public class Alert
    {
        public AlertTypeEnum AlertType { get; set; }
        public string Message { get; set; } 
    }
}
