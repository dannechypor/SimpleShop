using Microsoft.AspNetCore.Mvc;
using ForGoodTime.Components;
using Model.Models;
using Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Model.Entities;
using Microsoft.AspNetCore.Http;

namespace ForGoodTime.Components
{
    public class WishListSummary : ViewComponent
    {
        private readonly IWishListService wishListService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public WishListSummary(IWishListService _wishListService, IHttpContextAccessor _httpContextAccessor)
        {
            wishListService = _wishListService;
            httpContextAccessor = _httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var count = wishListService.GetCountOfWishList(userId);
            return View(new CountViewModel
            {
                Count = count
            });

        }
    }
}