using AutoMapper;
using Laundry.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet("frequent-customers")]
    public async Task<IActionResult> GetCustomersWhichOrderedTheMost()
    {
        try
        {
            var stats = await _statisticsService.GetCustomersWhichOrderedTheMostOftenAsync();
            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting stats" });
        }
    }

    [HttpGet("frequent-services")]
    public async Task<IActionResult> GetServicesOrderedTheMost()
    {
        try
        {
            var stats = await _statisticsService.GetCustomersWhichOrderedTheMostOftenAsync();
            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting stats" });
        }
    }

    [HttpGet("last-month-stats")]
    public async Task<IActionResult> GetLastMonthStat()
    {
        try
        {
            var stats = await _statisticsService.GetLastMonthOrdersStatisticsAsync();
            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting stats" });
        }
    }

    [HttpGet("last-year-stats")]
    public async Task<IActionResult> GetLastYearStat()
    {
        try
        {
            var stats = await _statisticsService.GetLastYearOrdersStatisticsAsync();
            if (stats == null)
            {
                return NotFound();
            }

            return Ok(stats);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting stats" });
        }
    }
}
