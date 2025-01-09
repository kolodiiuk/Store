using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _userService;
    
    public AuthController(IAuthService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "get_user")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        throw new NotImplementedException();
    }
}
