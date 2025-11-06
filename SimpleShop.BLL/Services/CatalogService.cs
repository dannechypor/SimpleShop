using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Services;

public class CatalogService(IUnitOfWork db) : ICatalogService
{
    private IUnitOfWork Database { get; } = db;

    public List<Product> GetProductByCategoryId(int id)
    {
        var product = Database.ProductRepository.GetProductsByCategoryId(id).ToList();
        return product;
    }

    public List<Product> GetProductByManufactureId(int id)
    {
        var manufacture = Database.ProductRepository.GetProductsByManufacturerId(id).ToList();
        return manufacture;
    }


    public ProductDto GetProductById(int id)
    {
        var product = Database.ProductRepository.All()
            .Where(x => x.Id == id)
            .Include(w => w.Category)
            .Include(z => z.Manufacturer)
            .Select(e => new ProductDto
            {
                Id = e.Id,
                Name = e.Name,
                Price = e.Price,
                ShortDescription = e.ShortDescription,
                LongDescription = e.LongDescription,
                ImageUrl_1 = e.ImageUrl_1,
                ImageUrl_2 = e.ImageUrl_2,
                ImageUrl_3 = e.ImageUrl_3,
                Category = e.Category,
                InStock = e.InStock,
                Date = e.Date,

                Hight = e.Hight,
                Width = e.Width,
                Deepth = e.Deepth,
                Manufacturer = e.Manufacturer
            });

        return product.FirstOrDefault();
    }
}