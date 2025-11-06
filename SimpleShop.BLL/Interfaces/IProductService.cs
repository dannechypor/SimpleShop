using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces;

public interface IProductService
{
    List<Product> GetAllProduct();
    List<Category> GetAllCategory();
    List<Manufacturer> GetAllManufacturer();
    Task<OperationDetails> AddProduct(ProductDto productDto);
    IEnumerable<TopSellingDto> GetTopSellingProducts();
    Task<OperationDetails> Delete(int id);
}