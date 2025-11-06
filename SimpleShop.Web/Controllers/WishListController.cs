using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Model.DTO;
using SimpleShop.Model.ViewModels;

namespace SimpleShop.Web.Controllers;

public class WishListController(IWishListService wishListService, IMapper mapper) : Controller
{
    [HttpGet]
    public async Task<IActionResult> AddToWishList(WishListItemViewModel model, int id)
    {
        var wishList = mapper.Map<WishListItemViewModel, WishListDto>(model);
        wishList.ProductId = id;
        wishList.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await wishListService.AddToWhishList(wishList);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var products = wishListService.GetAllProductIdByUser(userId);

        return View(new MyWishListViewModel
        {
            Products = products
        });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id == 0) return BadRequest();
        var result = await wishListService.DeleteRelationship(id, userId);
        if (result.Succedeed) return RedirectToAction("Index");

        return BadRequest();
    }
}