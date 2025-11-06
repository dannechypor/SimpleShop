using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Interfaces;
using Shop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;

        public string Id { get; set; }
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }

        private ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public static ShoppingCartService GetCart(IServiceProvider services)
        {
            var httpContext = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            var context = services.GetRequiredService<ApplicationDbContext>();

            var request = httpContext.Request;
            var response = httpContext.Response;

            var cardId = request.Cookies["CartId-cookie"] ?? Guid.NewGuid().ToString();

            response.Cookies.Append("CartId-cookie", cardId, new CookieOptions
            {
                Expires = DateTime.Now.AddMonths(2)
            });

            return new ShoppingCartService(context)
            {
                Id = cardId
            };
        }

        public async Task<int> AddToCartAsync(Product product, int qty = 1)
        {
            return await AddOrRemoveCart(product, qty);

        }

        public async Task<int> RemoveFromCartAsync(Product product)
        {
            return await AddOrRemoveCart(product, -1);
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            ShoppingCartItems = ShoppingCartItems ?? await _context.ShoppingCartItems
                .Where(e => e.ShoppingCartId == Id)
                .Include(e => e.Product)
                .ToListAsync();

            return ShoppingCartItems;
        }

        public async Task ClearCartAsync()
        {
            var shoppingCartItems = _context
                .ShoppingCartItems
                .Where(s => s.ShoppingCartId == Id);

            _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

            ShoppingCartItems = null; //reset
            await _context.SaveChangesAsync();
        }

        public async Task<(int ItemCount, decimal TotalAmmount)> GetCartCountAndTotalAmmountAsync()
        {
            var subTotal = ShoppingCartItems?
                .Select(c => c.Product.Price * c.Amount) ??
                await _context.ShoppingCartItems
                .Where(c => c.ShoppingCartId == Id)
                .Select(c => c.Product.Price * c.Amount)
                .ToListAsync();

            return (subTotal.Count(), subTotal.Sum());
        }

        private async Task<int> AddOrRemoveCart(Product product, int amount)
        {

            var shoppingCartItem = await _context.ShoppingCartItems
                            .SingleOrDefaultAsync(s => s.Product.Id == product.Id && s.ShoppingCartId == Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = Id,
                    Product = product,
                    Amount = 0
                };

                await _context.ShoppingCartItems.AddAsync(shoppingCartItem);
            }

            shoppingCartItem.Amount += amount;

            if (shoppingCartItem.Amount <= 0)
            {
                shoppingCartItem.Amount = 0;
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }

            await _context.SaveChangesAsync();

            ShoppingCartItems = null; // Reset

            return await Task.FromResult(shoppingCartItem.Amount);
        }

    }
}
