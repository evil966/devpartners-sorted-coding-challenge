using DevPartners.Sorted.Application.Configurations;
using DevPartners.Sorted.Application.Models;
using DevPartners.Sorted.Core.Entities;
using System.Net;
using System.Net.Http.Json;

namespace DevPartners.Sorted.Application.Services;

public class RainfallServices : IRainfallServices
{
    private readonly IHttpClientFactory _client;

    public RainfallServices(IHttpClientFactory client)
    {
        _client = client;
    }

    public async Task<ApiCallResult> Get(Uri endpoint, int stationId, int count)
    {
        var response = await InvokeRainfallApiRequest(endpoint, $"_limit={count}");
        var result = new ApiCallResult { StatusCode = (int)response.StatusCode, Message = response.ReasonPhrase };

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return result;
        }

        result.StatusCode = (int)HttpStatusCode.OK;
        result.Readings = await response.Content.ReadFromJsonAsync<Readings>();

        return result;
    }

    public async Task<ApiCallSummaryResult> GetSummary(Uri endpoint, int stationId, int hours)
    {
        var sinceRecordedTime = DateTime.UtcNow.AddHours(-hours).ToString("yyyy-MM-ddTHH:mm:ssZ");
        var response = await InvokeRainfallApiRequest(endpoint, $"since={sinceRecordedTime}");
        var result = new ApiCallSummaryResult { StatusCode = (int)response.StatusCode, Message = response.ReasonPhrase };

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return result;
        }

        double minimumMeasurement = 0.0;
        double maximumMeasurement = 0.0;
        double meanMeasurement = 0.0;
        int totalReadings = 0;

        var readings = await response.Content.ReadFromJsonAsync<Readings>();

        if (readings!.Items != null)
        {
            double sumOfMeasurements = 0;

            foreach (var value in readings.Items.Select(reading => reading.Value))
            {
                minimumMeasurement = value < minimumMeasurement ? value : minimumMeasurement;
                maximumMeasurement = value > maximumMeasurement ? value : maximumMeasurement;
                sumOfMeasurements += value;
                totalReadings++;
            }

            meanMeasurement = totalReadings > 0
                    ? sumOfMeasurements / totalReadings
                    : meanMeasurement;
        }

        result.StatusCode = (int)HttpStatusCode.OK;
        result.RainfallSummary = new RainfallSummary
        {
            StationId = stationId.ToString(),
            MeasurementsSince = Convert.ToDateTime(sinceRecordedTime),
            TotalReadings = totalReadings,
            MinimumMeasurement = minimumMeasurement,
            MaximumMeasurement = maximumMeasurement,
            MeanMeasurement = Math.Round(meanMeasurement, 2)
        };

        return result;
    }

    private async Task<HttpResponseMessage> InvokeRainfallApiRequest(Uri endpoint, string queryParameters)
    {
        var client = _client.CreateClient(RainfallServiceSettings.ServiceName);
        var response = await client.GetAsync($"{endpoint.AbsoluteUri}?{queryParameters}");

        return response;

    }
}
