using ForGoodTime.Views.Shared.Components.ShoppingCartSummary;
using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;

namespace ForGoodTime.Controllers;

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

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> AddToShoppingCartGet(int id)
    {
        var selectedCake = productRepository.GetById(id);
        if (selectedCake == null) return NotFound();

        await shoppingCart.AddToCartAsync(selectedCake);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromShoppingCart(int id)
    {
        var selectedCake = productRepository.GetById(id);
        if (selectedCake == null) return NotFound();

        await shoppingCart.RemoveFromCartAsync(selectedCake);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> RemoveAllCart()
    {
        await shoppingCart.ClearCartAsync();
        return RedirectToAction("Index");
    }
}