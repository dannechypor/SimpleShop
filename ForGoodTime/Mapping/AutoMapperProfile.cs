using Model.DTO;
using Model.Entities;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;

namespace ForGoodTime.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<UserDTO, ApplicationUser>();
            CreateMap<LoginViewModel, UserDTO>();
            CreateMap<RegisterViewModel, UserDTO>()
          .ForMember(dest => dest.Role, opts => opts.MapFrom(src => "user"));

            CreateMap<OrderDTO, Order>();
            CreateMap<OrderViewModel, OrderDTO>();
            CreateMap<MyOrderInfoDTO, Order>();

            CreateMap<MyOrderInfoDTO, MyOrderViewModel>();

            CreateMap<WishListDTO,WhishListItem>();
            CreateMap<WishListItemViewModel,WishListDTO>();

            CreateMap<MyWishListDTO,MyWishListViewModel>();
            CreateMap<MyWishListViewModel, MyWishListDTO>();

            CreateMap<ReviewViewModel,OrderDTO>();
            CreateMap<OrderDTO, ReviewViewModel>();

         


        }

    }
}
