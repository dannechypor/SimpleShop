using System.Collections.Generic;
using Model.DTO;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Interfaces;

public interface ICatalogService
{
    List<Product> GetProductByCategoryId(int id);
    List<Product> GetProductByManufactureId(int id);
    ProductDto GetProductById(int id);
}