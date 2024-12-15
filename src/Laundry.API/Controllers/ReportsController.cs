using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    [HttpGet("check")]
    public async Task<IActionResult> GetCheck(int orderId)
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