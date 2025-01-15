using AutoMapper;
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
    private readonly IMapper _mapper;
    
    public OrderController(IOrderService orderService,
        IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
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
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Order>> PlaceOrder([FromForm] CreateOrderDto orderDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<Order>> UpdateOrder([FromForm] OrderDtos orderDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut("status/{orderId}")]
    public async Task<ActionResult> UpdateOrderStatus(int orderId)
    {
        throw new NotImplementedException();
    }
}
