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
   public class ManufacturerService : IManufacturerService
    {
        private readonly IUnitOfWork Database;


        public ManufacturerService(IUnitOfWork db)
        {
            Database = db;
        }

        public List<Manufacturer> GetAllManufacturer()
        {
            var manufacturer = Database.ManufacturerRepositroy.GetAll().ToList();
            return manufacturer;
             
        }

        public async Task<OperationDetails> AddManufacturer(string Name)
        {
            if (Database.ManufacturerRepositroy.Get().Any(c => c.Name == Name))
            {
                return new OperationDetails(false, "The same category is already exist in database", "");
            }

            Database.ManufacturerRepositroy.Add(new Manufacturer { Name = Name });
            await Database.SaveAsync();
            return new OperationDetails(true, "Ok", "");
        }


        public async Task<OperationDetails> Delete(int id)
        {
            if (id == 0)
            {
                return new OperationDetails(false, "Id field is '0'", "");
            }
            Manufacturer manufacturer = Database.ManufacturerRepositroy.GetById(id);
            if (manufacturer == null)
            {
                return new OperationDetails(false, "Not found", "");
            }

            Database.ManufacturerRepositroy.Delete(manufacturer);
            await Database.SaveAsync();
            return new OperationDetails(true, "", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
