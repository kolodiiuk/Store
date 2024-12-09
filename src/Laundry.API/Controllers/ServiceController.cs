using Lab12.Controllers;
using Laundry.DataAccess;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laundry.Domain.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController( IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<Service>>> GetServices()
    {
        var query = _serviceService.GetAllServices();

        var services = await query.ToListAsync();

        return Ok(services);
    }

    [HttpGet(Name = "available")]
    public async Task<ActionResult<List<Service>>> GetAvailableServices()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}", Name = "GetService")]
    public async Task<ActionResult<Service>> GetService(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<ActionResult<Service>> CreateService([FromForm] CreateServiceDto serviceDto)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<ActionResult<Service>> UpdateService([FromForm] UpdateServiceDto serviceDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteService(int id)
    {
        throw new NotImplementedException();
    }
}