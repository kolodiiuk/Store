using Laundry.DataAccess.Repositories;
using Laundry.Domain.Contracts.Repositories;
using Laundry.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Laundry.DataAccess;

public static class ContainerConfigExtensions
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<ICouponRepository, CouponRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void RegisterMockRepositories(this IServiceCollection services)
    {
        services.AddScoped<IServiceRepository, MockServiceRepo>();
    }
}