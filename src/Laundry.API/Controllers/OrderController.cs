using Lab12.Controllers;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    
    [HttpGet("{id}", Name = "GetOrder")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromForm] CreateOrderDto basketDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder([FromForm] UpdateOrderDto basketDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }
}