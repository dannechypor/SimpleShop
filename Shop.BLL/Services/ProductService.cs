using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
   public class ProductService : IProductService
    {
        private readonly IUnitOfWork Database;


        public ProductService(IUnitOfWork db)
        {
            Database = db;
        }

        public List<Product> GetAllProduct()
        {
            return Database.ProductRepository.GetAll().ToList();
        }

        public List<Category> GetAllCategory()
        {
            var category = Database.CategoryRepository.GetAll().ToList();
            return category;
        }

        public List<Manufacturer> GetAllManufacturer()
        {
            var manufacturers = Database.ManufacturerRepositroy.GetAll().ToList();
            return manufacturers;
        }

        public async Task<OperationDetails> AddProduct(ProductDTO productDto)
        {
            
            Database.ProductRepository.Add(new Product
            { Name = productDto.Name,
                LongDescription = productDto.LongDescription,
                ShortDescription = productDto.ShortDescription,
               
                Hight = productDto.Hight,
                Width = productDto.Width,
                Deepth = productDto.Deepth,
                Price = productDto.Price,
                Date = productDto.Date,
                InStock = true ,
                ImageUrl_1 = productDto.ImageUrl_1,
                ImageUrl_2 = productDto.ImageUrl_2,
                ImageUrl_3 = productDto.ImageUrl_3,
                CategoryId = productDto.CategoryId,
                ManufacturerId = productDto.ManufacturerId });
            await Database.SaveAsync();
            return new OperationDetails(true, "Ok", "");
        }

        public IEnumerable<TopSellingDTO> GetTopSellings()
        {
            var top = Database.ProductRepository.GetTopSellings();
            return top;
        }

        public async Task<OperationDetails> Delete(int id)
        {
            if (id == 0)
            {
                return new OperationDetails(false, "Id field is '0'", "");
            }
            Product product = Database.ProductRepository.GetById(id);
            if (product == null)
            {
                return new OperationDetails(false, "Not found", "");
            }

            Database.ProductRepository.Delete(product);
            await Database.SaveAsync();
            return new OperationDetails(true, "", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
