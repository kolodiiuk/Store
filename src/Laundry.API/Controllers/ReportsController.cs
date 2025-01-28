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
    
    [HttpGet("cheque/{orderId}")]
    public async Task<IActionResult> GetCheque(int orderId)
    {
        try
        {
            var cheque = await _reportsService.CreateChequeAsync(orderId);

            return File(cheque, "application/pdf", "Cheque.pdf");
        }
        catch (Exception e)
        {
            
            return BadRequest(new ProblemDetails() { Title = $"Problem getting cheque for {orderId}" });
        }
    }

    [HttpGet("price_list")]
    public async Task<IActionResult> GetPriceList()
    {
        try
        {
            var priceList = await _reportsService.GetPriceListAsync();

            return File(priceList, "application/pdf", "PriceList.pdf");
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem  getting price list" });
        }
    }
}
