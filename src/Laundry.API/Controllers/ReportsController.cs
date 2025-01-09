using Laundry.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }
    
    [HttpGet("check")]
    public async Task<IActionResult> GetCheque(int orderId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("price_list")]
    public async Task<IActionResult> GetPriceList()
    {
        throw new NotImplementedException();
        // File method of ControllerBase
    }
}
