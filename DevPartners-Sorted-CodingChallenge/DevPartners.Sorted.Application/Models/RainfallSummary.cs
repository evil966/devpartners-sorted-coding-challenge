using System.ComponentModel.DataAnnotations;

namespace DevPartners.Sorted.Application.Models
{
    public class RainfallSummary
    {
        public string StationId { get; set; } = string.Empty;
        public DateTime? MeasurementsSince { get; set; }
        public int TotalReadings { get; set; }
        public double MinimumMeasurement { get; set; }
        public double MaximumMeasurement { get; set; }
        public double MeanMeasurement { get; set; }
    }
}
