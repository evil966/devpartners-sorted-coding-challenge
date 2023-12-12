using DevPartners.Sorted.Core.Entities;

namespace DevPartners.Sorted.Application.Services;

public interface IRainfallServices
{
    Task<Readings?> Get(Uri endpoint, int stationId);
}
