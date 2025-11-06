using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Model.ViewModels;

namespace SimpleShop.Web.Components;

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