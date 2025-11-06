using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;

namespace Shop.BLL.Services;

public class ManufacturerService(IUnitOfWork db) : IManufacturerService
{
    public List<Manufacturer> GetAllManufacturer()
    {
        var manufacturer = db.ManufacturerRepository.GetAll().ToList();
        return manufacturer;
    }

    public async Task<OperationDetails> AddManufacturer(string Name)
    {
        if (db.ManufacturerRepository.Get().Any(c => c.Name == Name))
            return new OperationDetails(false, "The same category is already exist in database", "");

        db.ManufacturerRepository.Add(new Manufacturer { Name = Name });
        await db.SaveAsync();
        return new OperationDetails(true, "Ok", "");
    }


    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");

        var manufacturer = db.ManufacturerRepository.GetById(id);
        if (manufacturer == null) return new OperationDetails(false, "Not found", "");

        db.ManufacturerRepository.Delete(manufacturer);
        await db.SaveAsync();
        return new OperationDetails(true, "", "");
    }
}