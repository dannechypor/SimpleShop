using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Interfaces;

public interface IProductService
{
    List<Product> GetAllProduct();
    PagedResultDto<Product> GetAllProductPaged(int pageNumber = 1, int pageSize = 10);
    Task<OperationDetails> AddProduct(ProductDto productDto);
    Task<OperationDetails> UpdateProduct(ProductDto productDto);
    IEnumerable<TopSellingDto> GetTopSellingProducts();
    Task<OperationDetails> Delete(int id);
}