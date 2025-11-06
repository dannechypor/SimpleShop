using System.Collections.Generic;
using Model.DTO;
using Model.Entities;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace Shop.DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    IEnumerable<Product> GetProductsByManufacturerId(int manufacturerId);
    IEnumerable<TopSellingDto> GetTopSellingProducts();
}