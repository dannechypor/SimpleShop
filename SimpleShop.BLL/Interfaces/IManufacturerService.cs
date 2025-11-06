using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IManufacturerService : IDisposable
    {
        List<Manufacturer> GetAllManufacturer();
        Task<OperationDetails> AddManufacturer(string Name);
        Task<OperationDetails> Delete(int id);
    }
}
