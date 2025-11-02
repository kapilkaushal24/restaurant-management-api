using AutoMapper;
using RestaurantManagement.Application.DTOs;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Restaurant mappings
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<UpdateRestaurantDto, Restaurant>();

        // MenuCategory mappings
        CreateMap<MenuCategory, MenuCategoryDto>();
        CreateMap<CreateMenuCategoryDto, MenuCategory>();

        // MenuItem mappings
        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<CreateMenuItemDto, MenuItem>();
        CreateMap<UpdateMenuItemDto, MenuItem>();

        // Order mappings
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateOrderDto, Order>();

        // OrderItem mappings
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));
        CreateMap<CreateOrderItemDto, OrderItem>();

        // User mappings
        CreateMap<User, AuthResponseDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
