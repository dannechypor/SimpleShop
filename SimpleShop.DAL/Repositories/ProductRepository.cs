using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories
{
   public class ProductRepository : Repository<Product> , IProductRepository
    {
        public ProductRepository(ApplicationDbContext db) : base(db)
        {

        }


        public IEnumerable<TopSellingDTO> GetTopSellings()
        {
            var products = context.Products.Include(o => o.Orders).Select(x => new TopSellingDTO
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

    
}
