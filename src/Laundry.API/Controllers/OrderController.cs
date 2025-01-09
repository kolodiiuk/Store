using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet("all")]
    public async Task<ActionResult<List<Order>>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Order>>> GetUsersOrders(int userId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}", Name = "GetOrder")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PlaceOrder([FromForm] CreateOrderDto basketDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder([FromForm] UpdateOrderDto basketDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut("status/{orderId}")]
    public async Task<ActionResult> UpdateOrderStatus(int orderId)
    {
        throw new NotImplementedException();
    }
    
    // [HttpDelete("{id}")]
    // public async Task<ActionResult> DeleteOrder(int id)
    // {
    //     throw new NotImplementedException();
    // }
}
