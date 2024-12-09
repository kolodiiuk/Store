using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    [HttpGet("{id}", Name = "GetBasket")]
    public async Task<ActionResult<BasketItem>> GetBasket(int id)
    {
        throw new NotImplementedException();
    }

    // [HttpPost]
    // public async Task<ActionResult<Basket>> CreateBasket([FromForm] CreateBasketDto basketDto)
    // {
    //     throw new NotImplementedException();
    // }

    //todo: What to return actually
    [HttpPut]
    public async Task<ActionResult<BasketItem>> UpdateBasket([FromForm] UpdateBasketDto basketDto)
    {
        throw new NotImplementedException();
    }
}