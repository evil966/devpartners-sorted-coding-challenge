﻿using DevPartners.Sorted.Api.Configurations;
using DevPartners.Sorted.Application.Models;
using DevPartners.Sorted.Application.Models.Errors;
using DevPartners.Sorted.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace DevPartners.Sorted.Api.Controllers;

[ApiController]
[Produces("application/json")]
public class RainfallController : ControllerBase
{
    private readonly IRainfallServices _services;
    public RainfallApiEndpointSettings Settings { get; set; }

    public RainfallController
    (
        IOptions<RainfallApiEndpointSettings> settings,
        IRainfallServices services
    )
    {
        Settings = settings.Value;
        _services = services;
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
        var rainfall = await _services.Get(uri, stationId);

        if (rainfall.Items.Count() == 0)
        {
            return new ObjectResult(
                        new ErrorResponse
                        {
                            Message = "No readings found for the specified stationId",
                            Detail = new List<ErrorDetail>()
                        }) 
                    { StatusCode = StatusCodes.Status404NotFound };
        }

        var rainfallReadings = rainfall.Items.Select(r =>
            new RainfallReading
            {
                AmountMeasured = r.Value,
                DateMeasured = r.DateTime
            });


        return new ObjectResult(
                    new RainfallReadingResponse
                    {
                        Readings = rainfallReadings.ToList()
                    }) 
                { StatusCode = StatusCodes.Status200OK };
    }
}


