using Store.Domain.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure;

public static class ContainerConfigExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}