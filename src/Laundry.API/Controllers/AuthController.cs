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
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        if (loginDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = $"Invalid user data" });
        }

        try
        {
            await _authService.LoginAsync(loginDto.Email, loginDto.Password);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem logging in a user {loginDto.Email}" });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid register data" });
        }

        var user = _mapper.Map<User>(registerDto);
        try
        {
            await _authService.RegisterAsync(user);
            return NoContent();
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
            var users = await _authService.GetUsersAsync();
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
            return BadRequest(new ProblemDetails() { Title = $"Problem getting a user {id}" });
        }
    }
}