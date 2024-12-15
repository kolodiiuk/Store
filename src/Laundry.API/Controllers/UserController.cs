using Laundry.API.Dto;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
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

    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser([FromForm] UpdateUserDto userDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}
