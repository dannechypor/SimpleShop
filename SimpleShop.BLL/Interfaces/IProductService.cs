using System.Collections.Generic;
using System.Threading.Tasks;
using Model.DTO;
using Shop.BLL.Infrastructure;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Interfaces;

public interface IProductService
{
    List<Product> GetAllProduct();
    PagedResultDto<Product> GetAllProductPaged(int pageNumber = 1, int pageSize = 10);
    Task<OperationDetails> AddProduct(ProductDto productDto);
    Task<OperationDetails> UpdateProduct(ProductDto productDto);
    IEnumerable<TopSellingDto> GetTopSellingProducts();
    Task<OperationDetails> Delete(int id);
}