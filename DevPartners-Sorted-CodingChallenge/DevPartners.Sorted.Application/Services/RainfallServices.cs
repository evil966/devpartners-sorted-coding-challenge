﻿using DevPartners.Sorted.Application.Configurations;
using DevPartners.Sorted.Application.Models;
using DevPartners.Sorted.Core.Entities;
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
        var client = _client.CreateClient(RainfallServiceSettings.ServiceName);
        var response = await client.GetAsync($"{endpoint.AbsoluteUri}?_limit={count}");
        var result = new ApiCallResult { StatusCode = (int)response.StatusCode, Message = response.ReasonPhrase };

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            return result;
        }

        result.StatusCode = (int)System.Net.HttpStatusCode.OK;
        result.Readings = await response.Content.ReadFromJsonAsync<Readings>();

        return result;
    }
}
