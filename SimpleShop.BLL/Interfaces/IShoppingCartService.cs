using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Interfaces;

/// <summary>
/// Manages the current user's shopping cart.
/// </summary>
public interface IShoppingCartService
{
    /// <summary>
    /// Identifier of the shopping cart (typically stored in a cookie).
    /// </summary>
    string Id { get; set; }

    /// <summary>
    /// Cached shopping cart items. Use <see cref="GetShoppingCartItemsAsync"/> to load/update.
    /// May be null if not yet loaded.
    /// </summary>
    IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

    /// <summary>
    /// Adds a product to the cart or increases its quantity.
    /// </summary>
    /// <param name="product">Product to add.</param>
    /// <param name="qty">Quantity to add (default 1).</param>
    /// <returns>The resulting quantity for that product in the cart.</returns>
    Task<int> AddToCartAsync(Product product, int qty = 1);

    /// <summary>
    /// Decreases the quantity of a product by 1. Removes the item if quantity reaches 0.
    /// </summary>
    /// <param name="product">Product to decrement.</param>
    /// <returns>The resulting quantity for that product in the cart.</returns>
    Task<int> RemoveFromCartAsync(Product product);

    /// <summary>
    /// Returns the list of items in the cart, loading them from storage if needed.
    /// </summary>
    Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync();

    /// <summary>
    /// Removes all items from the cart.
    /// </summary>
    Task ClearCartAsync();

    /// <summary>
    /// Returns total item count (sum of quantities) and total price for the cart.
    /// </summary>
    Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync();
}