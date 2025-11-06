using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Web.Views.Shared.Components.ShoppingCartSummary;

namespace SimpleShop.Web.Components;

public class ShoppingCartSummary : ViewComponent
{
    private readonly IShoppingCartService _shoppingCart;

    public ShoppingCartSummary(IShoppingCartService shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var shoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmmountAsync();
        var shoppingCartViewModel = new ShoppingCartViewModel
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
            ShoppingCartTotal = shoppingCartCountTotal.TotalAmmount
        };
        return View(shoppingCartViewModel);
    }
}