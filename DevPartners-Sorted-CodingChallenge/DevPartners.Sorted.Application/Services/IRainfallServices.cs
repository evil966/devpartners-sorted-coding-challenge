using DevPartners.Sorted.Application.Models;
using DevPartners.Sorted.Core.Entities;

namespace DevPartners.Sorted.Application.Services;

public interface IRainfallServices
{
    Task<ApiCallResult> Get(Uri endpoint, int stationId, int count);
    Task<ApiCallSummaryResult> GetSummary(Uri endpoint, int stationId, int hours);
}
