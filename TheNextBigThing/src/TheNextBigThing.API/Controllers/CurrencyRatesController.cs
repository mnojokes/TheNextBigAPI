using Microsoft.AspNetCore.Mvc;
using TheNextBigThing.Application.Services;
using TheNextBigThing.Domain.Responses;

namespace TheNextBigThing.API.Controllers;

#pragma warning disable 1591

[ApiController]
public class CurrencyRatesController : Controller
{
    private readonly CurrencyRateService _currencyRateService;

    public CurrencyRatesController(CurrencyRateService currencyRateService)
    {
        _currencyRateService = currencyRateService;
    }

#pragma warning restore 1591

    /// <summary>
    /// Get a list of currency rate changes for the day compared to the previous day.
    /// </summary>
    /// <remarks>
    /// Important: <br />
    /// * This API works with the dates up to the end of the year 2014; <br />
    /// * Results are sorted by change values from highest to lowest.
    /// </remarks>
    /// <response code="200">Changes successfully retrieved</response>
    /// <response code="400">Unable to retrieve data</response>
    /// <response code="500">Server error</response>
    [HttpGet("changes")]
    [Produces("application/xml")]
    [ProducesResponseType(typeof(RateChangesResponse), 200)]
    [ProducesResponseType(typeof(MessageResponse), 400)]
    [ProducesResponseType(typeof(MessageResponse), 500)]
    public async Task<IActionResult> GetRateChanges([FromQuery] DateTime date)
    {
        return Ok(await _currencyRateService.GetRateChanges(date));
    }
}
