using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleShop.DAL.EF;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Services;

public class WishListService(IUnitOfWork db, ApplicationDbContext context)
    : IWishListService
{
    public async Task<OperationDetails> AddToWhishList([FromForm] WishListDto wishListDto)
    {
        if (wishListDto == null)
            return new OperationDetails(false, "Wish list data is required", "");
            
        if (wishListDto.ProductId <= 0)
            return new OperationDetails(false, "Valid product ID is required", "");
            
        if (string.IsNullOrWhiteSpace(wishListDto.UserId))
            return new OperationDetails(false, "User ID is required", "");

        // Check if product already exists in user's wish list
        var existingItem = db.WishListRepository.Get()
            .FirstOrDefault(w => w.ProductId == wishListDto.ProductId && w.UserId == wishListDto.UserId);
            
        if (existingItem != null)
            return new OperationDetails(false, "Product already exists in wish list", "");

        // Verify product exists
        var product = db.ProductRepository.GetById(wishListDto.ProductId);
        if (product == null)
            return new OperationDetails(false, "Product not found", "");

        var whishList = new WishListItem
        {
            ProductId = wishListDto.ProductId,
            UserId = wishListDto.UserId
        };

        db.WishListRepository.Add(whishList);
        await db.SaveAsync();
        return new OperationDetails(true, "Product added to wish list successfully", "");
    }

    public List<Product> GetAllProductIdByUser(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new List<Product>();

        // Optimized query to avoid N+1 problem - load products in single query
        var productIds = db.WishListRepository.Get()
            .Where(e => e.UserId == userId)
            .Select(e => e.ProductId)
            .ToList();

        if (!productIds.Any())
            return new List<Product>();

        return db.ProductRepository.Get()
            .Where(p => productIds.Contains(p.Id))
            .ToList();
    }

    public int GetCountOfWishList(string? userId)
    {
        if (userId == null)
        {
            return 0;
        }

        var products = db.WishListRepository.All()
            .Where(e => e.UserId == userId)
            .Select(e => new Product
            {
                Id = e.ProductId
            }).ToList();

        return (products.Count);
    }

    public async Task<OperationDetails> DeleteRelationship(int productId, string userId)
    {
        if (productId == 0) return new OperationDetails(false, "ProductId field is '0'", "");
        if (string.IsNullOrWhiteSpace(userId)) return new OperationDetails(false, "UserId is required", "");

        var wishListItem = context.WishListItems
            .FirstOrDefault(p => p.ProductId == productId && p.UserId == userId);
        
        if (wishListItem == null) 
            return new OperationDetails(false, "Wish list item not found", "");
        
        context.WishListItems.Remove(wishListItem);
        await context.SaveChangesAsync();

        return new OperationDetails(true, "Item removed from wish list", "");
    }
}