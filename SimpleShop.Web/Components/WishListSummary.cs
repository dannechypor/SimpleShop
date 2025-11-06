using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Shop.BLL.Interfaces;

namespace ForGoodTime.Components;

public class WishListSummary(IWishListService wishListService, IHttpContextAccessor httpContextAccessor)
    : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var count = wishListService.GetCountOfWishList(userId);
        return Task.FromResult<IViewComponentResult>(View(new CountViewModel
        {
            Count = count
        }));
    }
}