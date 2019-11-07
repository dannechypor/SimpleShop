using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Shop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IWishListService
    {
        Task AddToWhishList([FromForm]WishListDTO wishListDto);
        List<Product> GetAllProductIdByUser(string userId);
        int GetCountOfWishList(string userId);
        Task<OperationDetails> DeleteRelationship(int productID, string userId);


    }
}
