using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    [HttpGet("{userId}", Name = "GetBasket")]
    public async Task<ActionResult<BasketItem>> GetBasket(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<BasketItem>> AddToBasket([FromForm] CreateBasketItemDto basketItemDto)
    {
        throw new NotImplementedException();
    }

    //todo: What to return actually
    [HttpPut]
    public async Task<ActionResult<BasketItem>> UpdateBasketItem([FromForm] UpdateBasketItemDto basketItemDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteBasketItem()
    {
        throw new NotImplementedException();
    }
}
