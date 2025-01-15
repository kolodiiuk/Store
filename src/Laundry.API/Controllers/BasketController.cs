using AutoMapper;
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
    private readonly IMapper _mapper;

    public BasketController(IBasketService basketService,
        IMapper mapper)
    {
        _basketService = basketService;
        _mapper = mapper;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<List<BasketItem>>> GetBasket(int userId)
    {
        try
        {
            var basket = await _basketService.GetBasket(userId);
            if (basket == null || !basket.Any())
            {
                return NotFound();
            }

            return Ok(basket.ToArray());
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting a basket for {userId}" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<BasketItem>> AddToBasket(
        [FromForm] CreateBasketItemDto basketItemDto)
    {
        if (basketItemDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid basket item data" });
        }

        try
        {
            var basketItem = _mapper.Map<BasketItem>(basketItemDto);

            var result = await _basketService.AddItemToBasketAsync(basketItem);
            return CreatedAtAction(nameof(GetBasket), new { userId = basketItemDto }, basketItem);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = "Problem adding basket item" });
        }
    }

    [HttpPut("quantity")]
    public async Task<ActionResult> UpdateQuantity(
        [FromForm] UpdateQuantityDto updateQuantityDto)
    {
        if (updateQuantityDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid quantity data" });
        }

        try
        {
            await _basketService.UpdateQuantity(
                updateQuantityDto.BasketItemId, updateQuantityDto.NewValue);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails()
                { Title = $"Problem updating quantity for basket item {updateQuantityDto.BasketItemId}" });
        }
    }

    [HttpDelete("{basketItemId}")]
    public async Task<ActionResult> DeleteBasketItem(int basketItemId)
    {
        try
        {
            await _basketService.DeleteItemFromBasket(basketItemId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = "Problem deleting basket item" });
        }
    }
}
