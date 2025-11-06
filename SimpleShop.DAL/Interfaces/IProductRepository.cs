using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Interfaces
{
   public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        IEnumerable<Product> GetProductsByManufacturerId(int manufacturerId);
        IEnumerable<TopSellingDTO> GetTopSellings();


    }
}
