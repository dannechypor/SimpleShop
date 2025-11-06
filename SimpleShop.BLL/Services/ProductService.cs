using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Services;

public class ProductService(IUnitOfWork db) : IProductService
{
    public List<Product> GetAllProduct()
    {
        return db.ProductRepository.GetAll().ToList();
    }

    public async Task<OperationDetails> AddProduct(ProductDto productDto)
    {
        if (productDto == null)
            return new OperationDetails(false, "Product data is required", "");
            
        if (string.IsNullOrWhiteSpace(productDto.Name))
            return new OperationDetails(false, "Product name is required", "");
            
        if (productDto.Price <= 0)
            return new OperationDetails(false, "Product price must be greater than 0", "");
            
        if (productDto.CategoryId <= 0)
            return new OperationDetails(false, "Valid category ID is required", "");
            
        if (productDto.ManufacturerId <= 0)
            return new OperationDetails(false, "Valid manufacturer ID is required", "");

        var category = db.CategoryRepository.GetById(productDto.CategoryId);
        if (category == null)
            return new OperationDetails(false, "Category not found", "");

        var manufacturer = db.ManufacturerRepository.GetById(productDto.ManufacturerId);
        if (manufacturer == null)
            return new OperationDetails(false, "Manufacturer not found", "");

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
        return new OperationDetails(true, "Product added successfully", "");
    }

    public IEnumerable<TopSellingDto> GetTopSellingProducts()
    {
        var top = db.ProductRepository.GetTopSellingProducts();
        return top;
    }

    public async Task<OperationDetails> UpdateProduct(ProductDto productDto)
    {
        if (productDto == null)
            return new OperationDetails(false, "Product data is required", "");
            
        if (productDto.Id <= 0)
            return new OperationDetails(false, "Valid product ID is required", "");
            
        if (string.IsNullOrWhiteSpace(productDto.Name))
            return new OperationDetails(false, "Product name is required", "");
            
        if (productDto.Price <= 0)
            return new OperationDetails(false, "Product price must be greater than 0", "");
            
        if (productDto.CategoryId <= 0)
            return new OperationDetails(false, "Valid category ID is required", "");
            
        if (productDto.ManufacturerId <= 0)
            return new OperationDetails(false, "Valid manufacturer ID is required", "");

        var product = db.ProductRepository.GetById(productDto.Id);
        if (product == null)
            return new OperationDetails(false, "Product not found", "");

        var category = db.CategoryRepository.GetById(productDto.CategoryId);
        if (category == null)
            return new OperationDetails(false, "Category not found", "");

        var manufacturer = db.ManufacturerRepository.GetById(productDto.ManufacturerId);
        if (manufacturer == null)
            return new OperationDetails(false, "Manufacturer not found", "");

        product.Name = productDto.Name;
        product.LongDescription = productDto.LongDescription;
        product.ShortDescription = productDto.ShortDescription;
        product.Hight = productDto.Hight;
        product.Width = productDto.Width;
        product.Deepth = productDto.Deepth;
        product.Price = productDto.Price;
        product.ImageUrl_1 = productDto.ImageUrl_1;
        product.ImageUrl_2 = productDto.ImageUrl_2;
        product.ImageUrl_3 = productDto.ImageUrl_3;
        product.CategoryId = productDto.CategoryId;
        product.ManufacturerId = productDto.ManufacturerId;
        product.InStock = productDto.InStock;

        db.ProductRepository.Update(product);
        await db.SaveAsync();
        return new OperationDetails(true, "Product updated successfully", "");
    }

    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");

        var product = db.ProductRepository.GetById(id);
        if (product == null) return new OperationDetails(false, "Product not found", "");

        db.ProductRepository.Delete(product);
        await db.SaveAsync();
        return new OperationDetails(true, "Product deleted successfully", "");
    }
}