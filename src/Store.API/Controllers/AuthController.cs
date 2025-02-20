using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Store.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Store.API.Dto;
using Store.Services.Services;

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

    [HttpPost("google-login")]
    public async Task<ActionResult<AuthResponse>> GoogleLogin([FromBody] GoogleLoginRequest request)
    {

        // 8. Return JWT token to frontend
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<User>> LoginAsync(LoginDto loginDto)
    {
        if (loginDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = $"Invalid user data" });
        }

        var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

        return Ok(result);
    }
    
    [Authorize(Roles= "AuthCustomer, Admin")]
    [HttpPost("logout")]
    public async Task<ActionResult> LogoutAsync(LogoutDto dto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<int>> RegisterAsync(RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid register data" });
        }

        var user = _mapper.Map<User>(registerDto);
        user.Role = Role.User;
        var userId = await _authService.RegisterAsync(user);

        return CreatedAtAction(nameof(GetUserAsync), new { id = userId }, null);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsersAsync()
    {
        var users = await _authService.GetAllUsersAsync();
        if (users == null)
        {
            return NotFound();
        }

        return Ok(users.ToArray());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUserAsync(int id)
    {
        if (id < 1)
        {
            return BadRequest(new ProblemDetails() {Title = "Invalid user id."});
        }
        
        var user = await _authService.GetUserAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
