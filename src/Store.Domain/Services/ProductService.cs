using Store.Domain.Utils;
using Store.Domain.Contracts.Repositories;
using Store.Domain.Contracts.Services;
using Store.Domain.Entities;

namespace Store.Domain.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
    {
        var result = await _repository.GetAllAvailableProductsAsync();
        result.OnFailure(() => throw new Exception(result.Error));

        return result.Value;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var result = await _repository.GetAllAsync();
        result.OnFailure(() => throw new Exception("Failure getting products"));

        return result.Value;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);
        result.OnFailure(() => throw new Exception("Failure getting products"));

        return result.Value;
    }

    public async Task<int> AddProductAsync(Product product)
    {
        var result = await _repository.CreateAsync(product);
        result.OnFailure(() => throw new Exception("Failure adding a new product"));

        return result.Value;
    }

    public async Task UpdateProductAsync(Product product)
    {
        var result = await _repository.UpdateAsync(product);
        result.OnFailure(() =>
            throw new Exception("Failure updating a product"));
    }

    public async Task DeleteProductAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        result.OnFailure(() =>
            throw new Exception("Failure deleting a product"));
    }
}