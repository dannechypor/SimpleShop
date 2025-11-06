using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;

namespace Shop.BLL.Services;

public class ProductService(IUnitOfWork db) : IProductService
{
    public List<Product> GetAllProduct()
    {
        return db.ProductRepository.GetAll().ToList();
    }

    public List<Category> GetAllCategory()
    {
        var category = db.CategoryRepository.GetAll().ToList();
        return category;
    }

    public List<Manufacturer> GetAllManufacturer()
    {
        var manufacturers = db.ManufacturerRepository.GetAll().ToList();
        return manufacturers;
    }

    public async Task<OperationDetails> AddProduct(ProductDTO productDto)
    {
        db.ProductRepository.Add(new Product
        {
            Name = productDto.Name,
            LongDescription = productDto.LongDescription,
            ShortDescription = productDto.ShortDescription,

            Hight = productDto.Hight,
            Width = productDto.Width,
            Deepth = productDto.Deepth,
            Price = productDto.Price,
            Date = productDto.Date,
            InStock = true,
            ImageUrl_1 = productDto.ImageUrl_1,
            ImageUrl_2 = productDto.ImageUrl_2,
            ImageUrl_3 = productDto.ImageUrl_3,
            CategoryId = productDto.CategoryId,
            ManufacturerId = productDto.ManufacturerId
        });
        await db.SaveAsync();
        return new OperationDetails(true, "Ok", "");
    }

    public IEnumerable<TopSellingDTO> GetTopSellingProducts()
    {
        var top = db.ProductRepository.GetTopSellingProducts();
        return top;
    }

    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");

        var product = db.ProductRepository.GetById(id);
        if (product == null) return new OperationDetails(false, "Not found", "");

        db.ProductRepository.Delete(product);
        await db.SaveAsync();
        return new OperationDetails(true, "", "");
    }
}