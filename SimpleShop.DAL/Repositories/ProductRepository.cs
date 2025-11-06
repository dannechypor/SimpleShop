using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Shop.DAL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.DAL.Repositories;
using SimpleShop.Model.Entities;

namespace Shop.DAL.Repositories;

public class ProductRepository(ApplicationDbContext db) : Repository<Product>(db), IProductRepository
{
    public IEnumerable<TopSellingDto> GetTopSellingProducts()
    {
        var products = Context.Products.Include(o => o.Orders).Select(x => new TopSellingDto
        {
            Price = x.Price, Image = x.ImageUrl_1, Name = x.Name, Count = x.Orders.Sum(q => q.Amount)
        }).OrderByDescending(z => z.Count).Take(10);

        return products;
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return GetAll().Where(product => product.CategoryId == categoryId).ToList();
    }

    public IEnumerable<Product> GetProductsByManufacturerId(int manufacturerId)
    {
        return GetAll().Where(product => product.ManufacturerId == manufacturerId).ToList();
    }
}