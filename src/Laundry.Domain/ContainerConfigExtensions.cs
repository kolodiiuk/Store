using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Services;
using Laundry.Domain.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Laundry.Domain;

public static class ContainerConfigExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<ICouponService, CouponService>();
        // services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IReportsService, ReportsService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<IDateTimeProvider, CurrentDateTimeProvider>();
    }
}