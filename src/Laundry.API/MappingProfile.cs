using AutoMapper;
using Laundry.API.Dto;
using Laundry.Domain.Entities;

namespace Laundry.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Service DTOs
        CreateMap<Service, CreateServiceDto>().ReverseMap();
        CreateMap<Service, UpdateServiceDto>().ReverseMap();
        // User DTOs
        CreateMap<User, RegisterDto>().ReverseMap();
        CreateMap<User, LoginDto>().ReverseMap();
        // Coupon DTOs
        CreateMap<Coupon, CreateCouponDto>().ReverseMap();
        CreateMap<Coupon, UpdateCouponDto>().ReverseMap();
        // Order DTOs
        CreateMap<Order, CreateOrderDto>().ReverseMap();
        // Basket DTOs
        CreateMap<BasketItem, CreateBasketItemDto>().ReverseMap();
        // Address DTOS
        CreateMap<Address, CreateAddressDto>().ReverseMap();
    }
}