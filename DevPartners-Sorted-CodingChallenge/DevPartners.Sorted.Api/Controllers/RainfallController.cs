using DevPartners.Sorted.Api.Configurations;
using DevPartners.Sorted.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System;
using static System.Net.WebRequestMethods;

namespace DevPartners.Sorted.Api.Controllers;

[ApiController]
[Produces("application/json")]
public class RainfallController : ControllerBase
{
    public RainfallApiEndpointSettings Settings { get; set; }

    public RainfallController(IOptions<RainfallApiEndpointSettings> settings)
    {
        Settings = settings.Value;
    }

    [HttpGet("rainfall/id/{stationId}/readings")]
    [SwaggerOperation(   
        Summary = "Get rainfall readings by station Id", 
        Description = "Retrieve the latest readings for the specified stationId", 
        OperationId = "get-rainfall", 
        Tags = new[] { "Rainfall" } 
    )]
    [SwaggerResponse(200, "A list of rainfall readings successfully retrieved", typeof(RainfallReadingResponse))]
    [SwaggerResponse(400, "Invalid request", typeof(ErrorResponse))]
    [SwaggerResponse(404, "No readings found for the specified stationId", typeof(ErrorResponse))]
    [SwaggerResponse(500, "Internal server error", typeof(ErrorResponse))]
    public async Task<IActionResult> Get(int stationId)
    {
        var uri = new Uri(Settings.Url.Replace("$stationid", stationId.ToString()));

        var items = new RainfallReadingResponse
        {
            Readings = new[] 
            {
                new RainfallReading { AmountMeasured = 7.6, DateMeasured = "2023-12-12T06:00:00Z" },
                new RainfallReading { AmountMeasured = 8.26, DateMeasured = "2023-12-12T05:45:00Z" }
            }
        };

        return new ObjectResult(items) { StatusCode = StatusCodes.Status200OK };
    }
}


