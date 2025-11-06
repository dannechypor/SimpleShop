using System.Collections.Generic;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    IEnumerable<Product> GetProductsByManufacturerId(int manufacturerId);
    IEnumerable<TopSellingDto> GetTopSellingProducts();
}