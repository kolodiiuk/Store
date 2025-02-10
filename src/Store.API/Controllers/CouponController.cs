using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;

namespace Store.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponService _couponService;
    
    private readonly IMapper _mapper;

    public CouponController(ICouponService couponService, IMapper mapper)
    {
        _couponService = couponService;
        _mapper = mapper;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet(Name = "all")]
    public async Task<ActionResult<List<UpdateCouponDto>>> GetCouponsAsync()
    {
        var result = await _couponService.GetAllCouponsAsync();
        result.OnFailure(() => BadRequest(result.Error));

        List<UpdateCouponDto> couponDtos = new List<UpdateCouponDto>();
        foreach (var c in result.Value)
        {
            UpdateCouponDto dto = new UpdateCouponDto()
            {
                Code = c.Code,
                EndDate = c.EndDate,
                StartDate = c.StartDate,
                Id = c.Id,
                Percentage = c.Percentage,
                UsedCount = c.UsedCount,
                ProductIds = c.ProductCoupons
                    .Select(coupon => coupon.ProductId)
                    .ToList()
            };
            couponDtos.Add(dto);
        }

        return Ok(couponDtos);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<int>> CreateCouponAsync(CreateCouponDto couponDto)
    {
        if (couponDto == null)
        {
            return BadRequest("Invalid coupon data");
        }

        var coupon = _mapper.Map<Coupon>(couponDto);
        var couponId = await _couponService.CreateCouponAsync(coupon, couponDto.ProductIds);

        return Ok(couponId);
    }

    [Authorize(Roles = "AuthCustomer")]
    [HttpPost("code")]
    public async Task<ActionResult> ValidateCouponAsync([FromBody] string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid code data." });
        }
        
        var isValid = await _couponService.ValidateCouponAsync(code);
        if (!isValid)
        {
            return BadRequest($"No such coupon {code}");
        }

        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Coupon>> UpdateCouponAsync(UpdateCouponDto couponDto)
    {
        if (couponDto == null)
        {
            return BadRequest("Invalid coupon data");
        }

        var coupon = _mapper.Map<Coupon>(couponDto);
        await _couponService.UpdateCouponAsync(coupon, couponDto.ProductIds ?? new List<int>());

        return Ok(coupon);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("{couponId:int}")]
    public async Task<ActionResult> DeleteCouponAsync(int couponId)
    {
        if (couponId < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid coupon id" });
        }
        
        await _couponService.DeleteCouponAsync(couponId);

        return NoContent();
    }
}
