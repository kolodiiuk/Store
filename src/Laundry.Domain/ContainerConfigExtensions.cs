using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Laundry.Domain;

public static class ContainerConfigExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<IReportsService, ReportsService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}