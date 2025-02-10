using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;

namespace Store.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;

    public AddressController(IAddressService addressService, IMapper mapper)
    {
        _addressService = addressService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<ActionResult<List<Address>>> GetAllAddressesAsync()
    {
        return Ok(await _addressService.GetAllAddressesAsync());
    }

    [Authorize(Roles = "User")]
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<Address>>> GetUserAddressesAsync(int userId)
    {
        if (userId < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }
        
        return Ok(await _addressService.GetUserAddressesAsync(userId));
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<ActionResult<Address>> CreateAddressAsync(CreateAddressDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }

        var address = _mapper.Map<Address>(addressDto);
        int id = await _addressService.CreateAddressAsync(address);
        address.Id = id;

        return CreatedAtAction(nameof(CreateAddressAsync), new { address.Id }, address);
    }

    [Authorize(Roles = "User")]
    [HttpPut]
    public async Task<ActionResult> UpdateAddressAsync(UpdateAddressDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }

        var address = await _addressService.GetAddressByIdAsync(addressDto.Id);
        if (address == null)
        {
            return BadRequest(new ProblemDetails()
                { Title = $"No address {addressDto.Id}" });
        }

        _mapper.Map(addressDto, address);
        await _addressService.UpdateAddressAsync(address);

        return Ok(address);
    }

    [Authorize(Roles = "User")]
    [HttpDelete("{addressId:int}")]
    public async Task<ActionResult> DeleteAddressAsync(int addressId)
    {
        if (addressId < 1)
        {
            return BadRequest(new ProblemDetails()
                { Title = "Invalid address data", Detail = "addressId < 0" });
        }

        await _addressService.DeleteAddressAsync(addressId);

        return Ok();
    }
}
