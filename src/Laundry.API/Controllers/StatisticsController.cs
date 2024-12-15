using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    [HttpGet("frequent_customers")]
    public async Task<IActionResult> GetCustomersWhichOrderedTheMost()
    {
        throw new NotImplementedException();
    }

    [HttpGet("frequent_services")]
    public async Task<IActionResult> GetServicesOrderedTheMost()
    {
        throw new NotImplementedException();
    }

    [HttpGet("last_month_stat")]
    public async Task<IActionResult> GetLastMonthStat()
    {
        throw new NotImplementedException();
    }

    [HttpGet("last_year_stat")]
    public async Task<IActionResult> GetLastYearStat()
    {
        throw new NotImplementedException();
    }
}