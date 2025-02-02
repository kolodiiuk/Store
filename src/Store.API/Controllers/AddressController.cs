using AutoMapper;
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

    [HttpGet("all")]
    public async Task<ActionResult<List<Address>>> GetAllAddressesAsync()
    {
        try
        {
            return Ok(await _addressService.GetAllAddressesAsync());
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting all addresses: {e.Message}" });
        }
    }
    
    [HttpGet("user/{userId:int}")]
    public async Task<ActionResult<List<Address>>> GetUserAddressesAsync(int userId)
    {
        try
        {
            return Ok(await _addressService.GetUserAddressesAsync(userId));
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting a user {userId} addresses: {e.Message}" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Address>> CreateAddressAsync([FromBody] CreateAddressDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }

        var address = _mapper.Map<Address>(addressDto);
        try
        {
            int id = await _addressService.CreateAddressAsync(address);
            address.Id = id;

            return CreatedAtAction(nameof(CreateAddressAsync), new { address.Id }, address);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem creating a new address: {e.Message}" });
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAddressAsync([FromBody] UpdateAddressDto addressDto)
    {
        if (addressDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid address data" });
        }

        var address = await _addressService.GetAddressByIdAsync(addressDto.Id);
        if (address == null)
        {
            return BadRequest(new ProblemDetails() 
                {Title = $"No address {addressDto.Id}"});
        }

        _mapper.Map(addressDto, address);
        try
        {
            await _addressService.UpdateAddressAsync(address);
            return Ok(address);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem updating an address: {e.Message}" });
        }
    }

    [HttpDelete("{addressId:int}")]
    public async Task<ActionResult> DeleteAddressAsync(int addressId)
    {
        if (addressId < 0)
        {
            return BadRequest(new ProblemDetails()
                { Title = "Invalid address data", Detail = "addressId < 0" });
        }

        try
        {
            await _addressService.DeleteAddressAsync(addressId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem deleting an address {addressId}: {e.Message}" });
        }
    }
}
