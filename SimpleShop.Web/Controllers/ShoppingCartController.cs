using Microsoft.AspNetCore.Mvc;
using Shop.DAL.Interfaces;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Web.Views.Shared.Components.ShoppingCartSummary;

namespace SimpleShop.Web.Controllers;

public class ShoppingCartController(IProductRepository productRepository, IShoppingCartService shoppingCart)
    : Controller
{
    public async Task<IActionResult> Index()
    {
        await shoppingCart.GetShoppingCartItemsAsync();
        var shoppingCartCountTotal = await shoppingCart.GetCartCountAndTotalAmmountAsync();
        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCart = shoppingCart,
            ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
            ShoppingCartTotal = shoppingCartCountTotal.TotalAmmount
        };

        return View(shoppingCartViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddToShoppingCart(int id)
    {
        var selectedCake = productRepository.GetById(id);
        if (selectedCake == null) return NotFound();

        await shoppingCart.AddToCartAsync(selectedCake);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> AddToShoppingCartGet(int id)
    {
        var selectedCake = productRepository.GetById(id);
        if (selectedCake == null) return NotFound();

        await shoppingCart.AddToCartAsync(selectedCake);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromShoppingCart(int id)
    {
        var selectedCake = productRepository.GetById(id);
        if (selectedCake == null) return NotFound();

        await shoppingCart.RemoveFromCartAsync(selectedCake);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> RemoveAllCart()
    {
        await shoppingCart.ClearCartAsync();
        return RedirectToAction(nameof(Index));
    }
}