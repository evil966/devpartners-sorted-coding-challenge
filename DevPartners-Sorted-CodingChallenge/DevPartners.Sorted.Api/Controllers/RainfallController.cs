using DevPartners.Sorted.Api.Configurations;
using DevPartners.Sorted.Application.Exceptions;
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
    public async Task<IActionResult> Get(int stationId, [FromQuery] string count="10")
    {
        try
        {
            var uri = new Uri(Settings.Url.Replace("$stationid", stationId.ToString()));
            var rainfall = await _services.Get(uri, stationId, count);

            if (rainfall.StatusCode != StatusCodes.Status200OK)
            {
                throw new RainfallServiceException($"{rainfall.StatusCode}|{rainfall.Message}");
            }

            if (rainfall.Readings.Items.Count() == 0)
            {
                throw new RainfallServiceException($"{StatusCodes.Status404NotFound}|No readings found for the specified stationId");
            }

            var rainfallReadings = rainfall.Readings.Items.Select(r =>
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
        catch (RainfallServiceException ex)
        {
            var error = ex.Message.Split('|');

            return new ObjectResult(
                            new ErrorResponse
                            {
                                Message = error[1],
                                Detail = new List<ErrorDetail>()
                            })
            { StatusCode = int.Parse(error[0]) };
        }
        catch (Exception ex)
        {
            return new ObjectResult(
                            new ErrorResponse
                            {
                                Message = ex.Message,
                                Detail = new List<ErrorDetail>()
                            })
            { StatusCode = StatusCodes.Status500InternalServerError };
        }


    }

}


