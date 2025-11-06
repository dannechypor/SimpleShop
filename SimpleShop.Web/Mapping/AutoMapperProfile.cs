using AutoMapper;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;
using SimpleShop.Model.ViewModels;

namespace SimpleShop.Web.Mapping;

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