using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Interfaces;

public interface IProductService
{
    List<Product> GetAllProduct();
    Task<OperationDetails> AddProduct(ProductDto productDto);
    Task<OperationDetails> UpdateProduct(ProductDto productDto);
    IEnumerable<TopSellingDto> GetTopSellingProducts();
    Task<OperationDetails> Delete(int id);
}