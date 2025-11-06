using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IShoppingCartService 
    {
        string Id { get; set; }
        IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<int> AddToCartAsync(Product product, int qty = 1);
        Task ClearCartAsync();
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync();
        Task<int> RemoveFromCartAsync(Product product);
        Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync();
    }
}
