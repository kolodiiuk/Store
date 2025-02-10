using AutoMapper;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;

namespace Store.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly IMapper _mapper;

    public BasketController(IBasketService basketService,
        IMapper mapper)
    {
        _basketService = basketService;
        _mapper = mapper;
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<List<BasketItem>>> GetBasketAsync(int userId)
    {
        if (userId < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid userId" });
        }
        
        var basket = await _basketService.GetBasketItemsAsync(userId);

        return Ok(basket.ToArray());
    }

    [HttpPost]
    public async Task<ActionResult<BasketItem>> AddToBasketAsync(CreateBasketItemDto basketItemDto)
    {
        if (basketItemDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid basket item data" });
        }

        var basketItem = _mapper.Map<BasketItem>(basketItemDto);
        var basketItemId = await _basketService.AddItemToBasketAsync(basketItem);

        return CreatedAtAction(nameof(AddToBasketAsync), 
            new { BasketItemId = basketItemId }, basketItem);
    }

    [HttpPut("quantity")]
    public async Task<ActionResult> UpdateQuantityAsync(UpdateQuantityDto updateQuantityDto)
    {
        if (updateQuantityDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid quantity data" });
        }

        await _basketService.UpdateQuantityAsync(
            updateQuantityDto.BasketItemId, updateQuantityDto.NewValue);

        return NoContent();
    }

    [HttpDelete("{basketItemId:int}")]
    public async Task<ActionResult> DeleteBasketItemAsync(int basketItemId)
    {
        if (basketItemId < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid userId" });
        }
        
        await _basketService.DeleteItemFromBasketAsync(basketItemId);

        return NoContent();
    }
}
