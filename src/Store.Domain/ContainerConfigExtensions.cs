using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Contracts.Services;
using Store.Domain.Services;
using Store.Domain.Utils;

namespace Store.Domain;

public static class ContainerConfigExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDateTimeProvider, CurrentDateTimeProvider>();
    }
}