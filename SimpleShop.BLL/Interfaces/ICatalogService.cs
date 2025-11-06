using System.Collections.Generic;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Interfaces;

public interface ICatalogService
{
    List<Product> GetProductByCategoryId(int id);
    List<Product> GetProductByManufactureId(int id);
    ProductDto GetProductById(int id);
}