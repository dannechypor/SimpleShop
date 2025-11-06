using Model.DTO;
using Model.Entities;
using Model.Models;
using AutoMapper;
using Model.ViewModels;

namespace ForGoodTime.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<UserDto, ApplicationUser>();
        CreateMap<LoginViewModel, UserDto>();
        CreateMap<RegisterViewModel, UserDto>()
            .ForMember(dest => 
                dest.Role, 
                opts => 
                    opts.MapFrom(src => "user"));

        CreateMap<OrderDto, Order>();
        CreateMap<OrderViewModel, OrderDto>();
        CreateMap<MyOrderInfoDto, Order>();

        CreateMap<MyOrderInfoDto, MyOrderViewModel>();

        CreateMap<WishListDto, WhishListItem>();
        CreateMap<WishListItemViewModel, WishListDto>();

        CreateMap<MyWishListDto, MyWishListViewModel>();
        CreateMap<MyWishListViewModel, MyWishListDto>();

        CreateMap<ReviewViewModel, OrderDto>();
        CreateMap<OrderDto, ReviewViewModel>();
    }
}