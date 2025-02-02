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
        try
        {
            var basket = await _basketService.GetBasketItemsAsync(userId);
            if (basket == null)
            {
                return BadRequest(new ProblemDetails()
                    {Title = $"No basket of a user {userId}"});
            }

            return Ok(basket.ToArray());
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting a basket for {userId}: {e.Message}" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<BasketItem>> AddToBasketAsync(
        [FromBody] CreateBasketItemDto basketItemDto)
    {
        if (basketItemDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid basket item data" });
        }

        try
        {
            var basketItem = _mapper.Map<BasketItem>(basketItemDto);

            var basketItemId = await _basketService.AddItemToBasketAsync(basketItem);
            return CreatedAtAction(nameof(AddToBasketAsync), new { BasketItemId = basketItemId }, basketItem);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem adding basket item: {e.Message}" });
        }
    }

    [HttpPut("quantity")]
    public async Task<ActionResult> UpdateQuantityAsync(
        [FromBody] UpdateQuantityDto updateQuantityDto)
    {
        if (updateQuantityDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid quantity data" });
        }

        try
        {
            await _basketService.UpdateQuantityAsync(
                updateQuantityDto.BasketItemId, updateQuantityDto.NewValue);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails()
                { Title = $"Problem updating quantity for basket item {updateQuantityDto.BasketItemId}: {e.Message}" });
        }
    }

    [HttpDelete("{basketItemId:int}")]
    public async Task<ActionResult> DeleteBasketItemAsync(int basketItemId)
    {
        try
        {
            await _basketService.DeleteItemFromBasketAsync(basketItemId);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem deleting basket item: {e.Message}" });
        }
    }
}