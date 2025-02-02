using AutoMapper;
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

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<List<Product>>> GetProductsAsync()
    {
        try
        {
            var query = await _productService.GetAllProductsAsync();
            if (query == null)
            {
                return NotFound();
            }

            return Ok(query);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting products: {e.Message}" });
        }
    }

    [HttpGet]
    [Route("available")]
    public async Task<ActionResult<List<Product>>> GetAvailableProductsAsync()
    {
        try
        {
            var products = await _productService.GetAvailableProductsAsync();

            return Ok(products.ToArray());
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting available products {e.Message}" });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProductAsync(int id)
    {
        try
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new ProblemDetails()
                    { Title = $"No product {id}" });
            }

            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem getting a product {id}: {e.Message}" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProductAsync([FromBody] CreateProductDto productDto)
    {
        if (productDto == null)
        {
            return BadRequest(new ProblemDetails() { Title = "Invalid product data" });
        }

        var product = _mapper.Map<Product>(productDto);
        try
        {
            int id = await _productService.AddProductAsync(product);
            product.Id = id;

            return CreatedAtAction(nameof(CreateProductAsync), new { product.Id }, product);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() { Title = $"Problem updating a product: {e.Message}" });
        }
    }

    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProductAsync([FromBody] UpdateProductDto productDto)
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
        try
        {
            await _productService.UpdateProductAsync(product);
            return Ok(product);
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem  updating a product {product.Id}: {e.Message}" });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProductAsync(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(new ProblemDetails() 
                { Title = $"Problem deleting a product {id}: {e.Message}" });
        }
    }
}