using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IProductService : IDisposable
    {
        List<Product> GetAllProduct();
        List<Category> GetAllCategory();
        List<Manufacturer> GetAllManufacturer();
        Task<OperationDetails> AddProduct(ProductDTO productDto);
        IEnumerable<TopSellingDTO> GetTopSellings();
        Task<OperationDetails> Delete(int id);
    }
}
