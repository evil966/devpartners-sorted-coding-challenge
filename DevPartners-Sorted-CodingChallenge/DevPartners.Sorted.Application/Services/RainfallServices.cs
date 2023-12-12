using DevPartners.Sorted.Core.Entities;
using System.Net.Http;
using System.Net.Http.Json;

namespace DevPartners.Sorted.Application.Services;

public class RainfallServices : IRainfallServices
{
    private readonly IHttpClientFactory _client;

    public RainfallServices(IHttpClientFactory client)
    {
        _client = client;
    }

    public async Task<Readings?> Get(Uri endpoint, int stationId)
    {
        var client = _client.CreateClient("rainfallservice");
        var response = await client.GetAsync(endpoint.AbsoluteUri);

        var content = await response.Content.ReadFromJsonAsync<Readings>();

        if (content != null)
        {
            return content;
        }
        return default;
    }
}
