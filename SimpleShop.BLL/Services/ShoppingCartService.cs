using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Services;

public class ShoppingCartService(ApplicationDbContext context) : IShoppingCartService
{
    private const string CartCookieName = "CartId-cookie";
    private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public string Id { get; set; }
    public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public Task<int> AddToCartAsync(Product product, int qty = 1)
    {
        return ChangeItemQuantityAsync(product, qty);
    }

    public Task<int> RemoveFromCartAsync(Product product)
    {
        return ChangeItemQuantityAsync(product, -1);
    }

    public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
    {
        EnsureCartId();

        if (ShoppingCartItems.Any())
            return ShoppingCartItems;

        ShoppingCartItems = await _context.ShoppingCartItems
            .Where(e => e.ShoppingCartId == Id)
            .Include(e => e.Product)
            .ToListAsync();

        return ShoppingCartItems;
    }

    public async Task ClearCartAsync()
    {
        EnsureCartId();

        var shoppingCartItems = _context
            .ShoppingCartItems
            .Where(s => s.ShoppingCartId == Id);

        _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

        await _context.SaveChangesAsync();

        ShoppingCartItems = new List<ShoppingCartItem>();
    }

    public async Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync()
    {
        EnsureCartId();

        var items = (await GetShoppingCartItemsAsync()).ToList();

        var itemCount = items.Sum(c => c.Amount);
        var totalAmount = items.Sum(c => c.Product.Price * c.Amount);

        return (itemCount, totalAmount);
    }

    /// <summary>
    ///     Factory method to get a ShoppingCartService bound to the current HTTP context.
    /// </summary>
    public static ShoppingCartService GetCart(IServiceProvider services)
    {
        var httpContextAccessor = services.GetRequiredService<IHttpContextAccessor>();
        var context = services.GetRequiredService<ApplicationDbContext>();

        var httpContext = httpContextAccessor.HttpContext
                          ?? throw new InvalidOperationException("No HttpContext available.");

        var request = httpContext.Request;
        var response = httpContext.Response;

        var cartId = request.Cookies[CartCookieName];

        if (!string.IsNullOrWhiteSpace(cartId))
            return new ShoppingCartService(context)
            {
                Id = cartId
            };
        cartId = Guid.NewGuid().ToString("N");

        response.Cookies.Append(CartCookieName, cartId, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddMonths(2),
            HttpOnly = true,
            IsEssential = true,
            Path = "/",                
            SameSite = SameSiteMode.None,
            Secure = true              
        });

        return new ShoppingCartService(context)
        {
            Id = cartId
        };
    }

    /// <summary>
    ///     Internal helper to adjust quantity of a product (add/remove).
    /// </summary>
    private async Task<int> ChangeItemQuantityAsync(Product product, int delta)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (delta == 0) return 0;

        EnsureCartId();

        var shoppingCartItem = await _context.ShoppingCartItems
            .Include(s => s.Product)
            .SingleOrDefaultAsync(s =>
                s.Product.Id == product.Id && s.ShoppingCartId == Id);

        if (shoppingCartItem == null)
        {
            if (delta < 0) return 0;

            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = Id,
                Product = product,
                Amount = 0
            };

            await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
        }

        shoppingCartItem.Amount += delta;

        if (shoppingCartItem.Amount <= 0)
        {
            _context.ShoppingCartItems.Remove(shoppingCartItem);
            shoppingCartItem.Amount = 0;
        }

        await _context.SaveChangesAsync();

        ShoppingCartItems = new List<ShoppingCartItem>();

        return shoppingCartItem.Amount;
    }

    private void EnsureCartId()
    {
        if (string.IsNullOrWhiteSpace(Id))
            throw new InvalidOperationException("Shopping cart Id has not been initialized.");
    }
}