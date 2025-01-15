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
    
    [HttpGet("cheque")]
    public async Task<IActionResult> GetCheque(int orderId, string email)
    {
        await _reportsService.SendChequeWithEmail(orderId, email);

        return Ok();
    }

    [HttpGet("price_list")]
    public async Task<IActionResult> GetPriceList()
    {
        var priceList = await _reportsService.GetPriceListAsync();

        return File(priceList, "application/pdf", "PriceList.pdf");
    }
}
