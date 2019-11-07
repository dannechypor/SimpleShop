using Microsoft.AspNetCore.Mvc;
using ForGoodTime.Components;
using Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForGoodTime.Views.Shared.Components.ShoppingCartSummary;

namespace ForGoodTime.Components
{
    public class OrderSummary : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCart;

        public OrderSummary(IShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ShoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmmountAsync();
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartItemsTotal = ShoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = ShoppingCartCountTotal.TotalAmmount
            };
            return View(shoppingCartViewModel);
        }
    }
}
