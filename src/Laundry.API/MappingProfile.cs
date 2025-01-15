using AutoMapper;
using Laundry.API.Dto;
using Laundry.Domain.Entities;

namespace Laundry.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Service, CreateServiceDto>().ReverseMap();
        CreateMap<Service, UpdateServiceDto>().ReverseMap();
        
        // CreateMap<User, RegisterDto>().ReverseMap();
        // CreateMap<User, LoginDto>().ReverseMap();
        CreateMap<Coupon, CreateCouponDto>().ReverseMap();
        CreateMap<Coupon, UpdateCouponDto>().ReverseMap();
        CreateMap<Order, CreateOrderDto>().ReverseMap();
        CreateMap<BasketItem, CreateBasketItemDto>().ReverseMap();
    }
}