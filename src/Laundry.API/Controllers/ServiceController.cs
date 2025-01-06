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
        var query = await _serviceService.GetAllServicesAsync();

        var services = query.ToList();
        var servicesDtos = _mapper.Map<List<UpdateServiceDto>>(services);

        return Ok(servicesDtos.ToArray()); 
    }

    [HttpGet]
    [Route("available")]
    public async Task<ActionResult<List<Service>>> GetAvailableServices()
    {
        var query = await _serviceService.GetAllAvailableServicesAsync();

        var services = query.ToList();
        var servicesDtos = _mapper.Map<List<UpdateServiceDto>>(services);

        return Ok(servicesDtos.ToArray());
    }

    [HttpGet("{id}", Name = "getService")]
    public async Task<ActionResult<Service>> GetService(int id)
    {
        var service = await _serviceService.GetServiceByIdAsync(id);

        if (service == null)
        {
            return NotFound();
        }

        return Ok(service);
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
            _serviceService.UpdateServiceAsync(service);
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
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem deleting a service {id}" });
        }
        
        return Ok();
    }
}