using AutoMapper;
using Model.DTO;
using Model.Entities;
using Model.ViewModels;
using SimpleShop.Model.Entities;

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

        CreateMap<WishListDto, WishListItem>();
        CreateMap<WishListItemViewModel, WishListDto>();

        CreateMap<MyWishListDto, MyWishListViewModel>();
        CreateMap<MyWishListViewModel, MyWishListDto>();

        CreateMap<ReviewViewModel, OrderDto>();
        CreateMap<OrderDto, ReviewViewModel>();

        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewDto, Review>();
    }
}