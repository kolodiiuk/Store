using AutoMapper;
using Laundry.API.Dto;
using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;
    private readonly IMapper _mapper;

    public ServiceController(IServiceService serviceService, IMapper mapper)
    {
        _serviceService = serviceService;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<Service>>> GetServices()
    {
        try
        {
            var query = await _serviceService.GetAllServicesAsync();
            if (query == null)
            {
                return NotFound();
            }

            return Ok(query);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting all services" });
        }
    }

    [HttpGet]
    [Route("available")]
    public async Task<ActionResult<List<Service>>> GetAvailableServices()
    {
        try
        {
            var services = await _serviceService.GetAllAvailableServicesAsync();

            return Ok(services.ToArray());
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting available services {e.Message}" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Service>> GetService(int id)
    {
        try
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            return Ok(service);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem getting a service {id}" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Service>> CreateService([FromForm] CreateServiceDto serviceDto)
    {
        if (serviceDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid service data" });
        }
        var service = _mapper.Map<Service>(serviceDto);
        try
        {
            int id = await _serviceService.AddServiceAsync(service);
            service.Id = id;

            return CreatedAtRoute("getService", new { Id = service.Id }, service);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = "Problem updating a service" });
        }
    }

    [HttpPut]
    public async Task<ActionResult<Service>> UpdateService([FromForm] UpdateServiceDto serviceDto)
    {
        if (serviceDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid service data" });
        }
        var service = await _serviceService.GetServiceByIdAsync(serviceDto.Id);
        if (service == null)
        {
            return NotFound();
        }
        _mapper.Map(serviceDto, service);
        try
        {
            await _serviceService.UpdateServiceAsync(service);
            return Ok(service);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem  updating a service {service.Id}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteService(int id)
    {
        try
        {
            await _serviceService.DeleteServiceAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem deleting a service {id}" });
        }
    }
}