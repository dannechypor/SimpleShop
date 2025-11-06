using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Web.Views.Shared.Components.ShoppingCartSummary;

namespace SimpleShop.Web.Controllers;

public class ShoppingCartController(IProductRepository productRepository, IShoppingCartService shoppingCart)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var shoppingCartCountTotal = await shoppingCart.GetCartCountAndTotalAmmountAsync();
        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCart = shoppingCart,
            ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
            ShoppingCartTotal = shoppingCartCountTotal.TotalAmmount
        };

        return View(shoppingCartViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddToShoppingCart(int id)
    {
        var product = productRepository.GetById(id);
        if (product == null) return NotFound();

        await shoppingCart.AddToCartAsync(product);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveFromShoppingCart(int id)
    {
        var product = productRepository.GetById(id);
        if (product == null) return NotFound();

        await shoppingCart.RemoveFromCartAsync(product);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveAllCart()
    {
        await shoppingCart.ClearCartAsync();
        return RedirectToAction(nameof(Index));
    }
}