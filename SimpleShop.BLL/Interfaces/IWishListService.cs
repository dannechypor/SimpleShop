using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Shop.BLL.Infrastructure;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Interfaces;

public interface IWishListService
{
    Task<OperationDetails> AddToWhishList([FromForm] WishListDto wishListDto);
    List<Product> GetAllProductIdByUser(string userId);
    int GetCountOfWishList(string? userId);
    Task<OperationDetails> DeleteRelationship(int productID, string userId);
}