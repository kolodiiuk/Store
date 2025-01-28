using AutoMapper;
using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AddressController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Address>>> GetAllAddresses()
    {
        try
        {
            var addresses = await _authService.GetAllAddressesAsync();
            if (addresses == null)
            {
                return Ok(new List<Address>() {});
            }

            return Ok(addresses);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting all addresses" });
        }
    }
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<Address>>> GetUserAddresses(int userId)
    {
        try
        {
            var addresses = await _authService.GetUserAddressesAsync(userId);
            if (addresses == null)
            {
                return Ok(new List<Address> { });
            }

            return Ok(addresses);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting a user {userId} addresses" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Service>> CreateAddress([FromForm] CreateAddressDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }

        var address = _mapper.Map<Address>(addressDto);
        try
        {
            int id = await _authService.CreateAddressAsync(address);
            address.Id = id;

            return CreatedAtRoute("getService", new { Id = address.Id }, address);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = "Problem creating a new address" });
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAddress([FromForm] UpdateAddressDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }

        var address = await _authService.GetAddressByIdAsync(addressDto.Id);
        if (address == null)
        {
            return NotFound();
        }

        _mapper.Map(addressDto, address);
        try
        {
            await _authService.UpdateAddressAsync(address);
            return Ok(address);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = "Problem updating an address" });
        }
    }

    [HttpDelete("{addressId:int}")]
    public async Task<ActionResult> DeleteAddress([FromRoute] int addressId)
    {
        if (addressId < 0)
        {
            return BadRequest(new ProblemDetails()
                { Title = "Invalid address data", Detail = "addressId < 0" });
        }

        try
        {
            await _authService.DeleteAddressAsync(addressId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem deleting an address {addressId}" });
        }
    }
}