using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Store.API.Controllers;

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
    public async Task<ActionResult<List<Order>>> GetAllOrdersAsync()
    {
        var orders = await _orderService.GetAllOrdersAsync();

        return Ok(orders);
    }

    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<Order>>> GetUserOrdersAsync(int userId)
    {
        if (userId < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid user id." });
        }
        
        var orders = await _orderService.GetUserOrdersAsync(userId);

        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrderAsync(int id)
    {
        if (id < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid order id." });
        }
        
        var order = await _orderService.GetOrderAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PlaceOrderAsync(CreateOrderDto orderDto)
    {
        if (orderDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid order data" });
        }

        var order = await _orderService.PlaceOrderAsync(orderDto);

        return CreatedAtRoute(new { order.Id }, order);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateOrder(Order order)
    {
        await _orderService.UpdateOrderAsync(order);

        return Ok();
    }

    [HttpGet("orderItems/{orderId:int}")]
    public async Task<ActionResult<List<OrderItem>>> GetOrderItems(int orderId)
    {
        if (orderId < 1)
        {
            return BadRequest(new ProblemDetails() { Title = $"Invalid orderId: {orderId}" });
        }

        var orderItems = await _orderService.GetOrderItemsAsync(orderId);
        if (orderItems == null)
        {
            return Ok(new List<OrderItem>());
        }

        return Ok(orderItems);
    }
}
