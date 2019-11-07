using Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForGoodTime.Views.Shared.Components.ShoppingCartSummary
{
  public  class ShoppingCartViewModel
    {
        public IShoppingCartService ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public int ShoppingCartItemsTotal { get; set; }
    }
}
