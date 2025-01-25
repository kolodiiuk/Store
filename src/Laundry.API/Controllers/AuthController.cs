using AutoMapper;
using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService,
        IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginDto loginDto)
    {
        if (loginDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = $"Invalid user data" });
        }

        try
        {
            var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem logging in a user {loginDto.Email}" });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<int>> Register(RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid register data" });
        }

        try
        {
            var user = _mapper.Map<User>(registerDto);
            user.Role = Role.User;
            var userId = await _authService.RegisterAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = userId }, null);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = "Problem registering a new user" });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        try
        {
            var users = await _authService.GetAllUsersAsync();
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users.ToArray());
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting all services" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        try
        {
            var user = await _authService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting a {nameof(User)} {id}" });
        }
    }

    [HttpGet("address/{userId}")]
    public async Task<ActionResult<List<Address>>> GetUserAddresses(int userId)
    {
        try
        {
            var addresses = await _authService.GetUserAddresses(userId);
            if (addresses == null)
            {
                return NotFound();
            }

            return Ok(addresses);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting a user {userId} addresses" });
        }
    }
    
    [HttpPost("address")]
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
}
