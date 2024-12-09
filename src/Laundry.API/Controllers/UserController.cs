using Lab12.Controllers;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateUser([FromForm] CreateUserDto userDto)
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
