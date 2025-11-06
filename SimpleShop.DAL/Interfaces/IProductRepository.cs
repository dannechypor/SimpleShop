using System.Collections.Generic;
using Model.DTO;
using Model.Entities;

namespace Shop.DAL.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    IEnumerable<Product> GetProductsByManufacturerId(int manufacturerId);
    IEnumerable<TopSellingDTO> GetTopSellingProducts();
}