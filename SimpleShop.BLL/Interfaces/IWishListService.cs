using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Interfaces;

public interface IWishListService
{
    Task<OperationDetails> AddToWhishList([FromForm] WishListDto wishListDto);
    List<Product> GetAllProductIdByUser(string userId);
    int GetCountOfWishList(string? userId);
    Task<OperationDetails> DeleteRelationship(int productID, string userId);
}