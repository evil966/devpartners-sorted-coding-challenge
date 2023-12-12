using DevPartners.Sorted.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace DevPartners.Sorted.Api.Controllers;

[ApiController]
public class RainfallController : ControllerBase
{
    [HttpGet("rainfall/id/{stationId}/readings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IEnumerable<ReadingsResponse>> Get(int stationId)
    {
        return new[] { new ReadingsResponse { Context = "http://environment.data.gov.uk/flood-monitoring/meta/context.jsonld" } };
    }
}


