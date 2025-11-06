using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using ForGoodTime.Views.Shared.Components.ShoppingCartSummary;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Web.Views.Shared.Components.ShoppingCartSummary;

namespace ForGoodTime.Components;

public class OrderSummary(IShoppingCartService shoppingCart) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
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
}