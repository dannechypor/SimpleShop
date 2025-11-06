using Model.Entities;
using Shop.BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Interfaces;

public interface IManufacturerService
{
    List<Manufacturer> GetAllManufacturer();
    Task<OperationDetails> AddManufacturer(string name);
    Task<OperationDetails> Update(int id, string name);
    Task<OperationDetails> Delete(int id);
}