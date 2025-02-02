using AutoMapper;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;

namespace Store.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> LoginAsync(LoginDto loginDto)
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
            return BadRequest(new ProblemDetails()
                { Title = $"Problem logging in a user {loginDto.Email}: {e.Message}" });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<int>> RegisterAsync(RegisterDto registerDto)
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
            return CreatedAtAction(nameof(GetUserAsync), new { id = userId }, null);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem registering a new user: {e.Message}" });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsersAsync()
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
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting all users: {e.Message}" });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserAsync(int id)
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
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting a {nameof(User)} {id}: {e.Message}" });
        }
    }
}
