using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.DAL.EF;

namespace Shop.BLL.Services;

public class WishListService(IUnitOfWork db, ApplicationDbContext context)
    : IWishListService
{
    public async Task AddToWhishList([FromForm] WishListDTO wishListDto)
    {
        var whishList = new WhishListItem
        {
            ProductId = wishListDto.ProductId,
            UserId = wishListDto.UserId
        };

        db.WishListRepository.Add(whishList);
        await db.SaveAsync();
    }

    public List<Product> GetAllProductIdByUser(string userId)
    {
        var products = db.WishListRepository.All().Where(e => e.UserId == userId).ToList();
        //.Include(e => e.ProductId)
        //.Select(e => new Product
        //{
        //    Id = e.ProductId
        //})
        //.ToList();


        return products.Select(item => db.ProductRepository.GetAll().Where(e => e.Id == item.ProductId))
            .Select(tmp => tmp.First()).ToList();
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
        var product = context.WhishListItems.FirstOrDefault(p => p.ProductId == productId);
        var supplier = context.WhishListItems.FirstOrDefault(s => s.UserId == userId);
        
        context.WhishListItems.Remove(supplier);
        context.WhishListItems.Remove(product);
        
        await context.SaveChangesAsync();

        return new OperationDetails(true, "", "");
    }
}