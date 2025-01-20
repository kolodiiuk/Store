using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Laundry.DataAccess;

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
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}