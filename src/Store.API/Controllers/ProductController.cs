using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Store.API.Dto;

namespace Store.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<Product>>> GetProductsAsync()
    {
        var query = await _productService.GetAllProductsAsync();
        if (query == null)
        {
            return NotFound();
        }

        return Ok(query);
    }

    [HttpGet]
    [Route("available")]
    public async Task<ActionResult<List<Product>>> GetAvailableProductsAsync()
    {
        throw new Exception();
        var products = await _productService.GetAvailableProductsAsync();
        
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        if (id < 1)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid product id." });
        }
        
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return BadRequest(new ProblemDetails()
                { Title = $"No product {id}" });
        }

        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync(CreateProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid product data" });
        }

        var product = _mapper.Map<Product>(productDto);
        int id = await _productService.AddProductAsync(product);
        product.Id = id;

        return CreatedAtAction(nameof(CreateProductAsync), new { product.Id }, product);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProductAsync(UpdateProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid product data" });
        }

        var product = await _productService.GetProductByIdAsync(productDto.Id);
        if (product == null)
        {
            return NotFound();
        }

        _mapper.Map(productDto, product);
        await _productService.UpdateProductAsync(product);

        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
        if (id < 1)
        {
            return BadRequest(new ProblemDetails() {Title = "Invalid product id."});
        }
        
        await _productService.DeleteProductAsync(id);

        return Ok();
    }
}
