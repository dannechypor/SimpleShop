using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL.Interfaces
{
    public interface ICatalogService
    {
         List<Category> GetAllCategory();
         List<Product> GetAllProduct();
         List<Manufacturer> GetAllManufacturer();
         List<Product> GetProductByCategoryId(int id);
         List<Product> GetProductByManufactureId(int id);
         ProductDTO GetProductById(int id);

    }
}
