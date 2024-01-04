using DevPartners.Sorted.Core.Entities;

namespace DevPartners.Sorted.Application.Models;

public class ApiCallSummaryResult : ApiCallBaseResult
{
    public RainfallSummary? RainfallSummary { get; set; }
}
