using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Services;

public class ManufacturerService(IUnitOfWork db) : IManufacturerService
{
    public List<Manufacturer> GetAllManufacturer()
    {
        var manufacturer = db.ManufacturerRepository.GetAll().ToList();
        return manufacturer;
    }

    public async Task<OperationDetails> AddManufacturer(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return new OperationDetails(false, "Manufacturer name is required", "");
            
        if (db.ManufacturerRepository.Get().Any(c => c.Name == Name))
            return new OperationDetails(false, "The same manufacturer already exists in database", "");

        db.ManufacturerRepository.Add(new Manufacturer { Name = Name });
        await db.SaveAsync();
        return new OperationDetails(true, "Manufacturer added successfully", "");
    }


    public async Task<OperationDetails> Update(int id, string name)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");
        if (string.IsNullOrWhiteSpace(name))
            return new OperationDetails(false, "Manufacturer name is required", "");
            
        var manufacturer = db.ManufacturerRepository.GetById(id);
        if (manufacturer == null) return new OperationDetails(false, "Manufacturer not found", "");

        // Check if another manufacturer with the same name exists
        if (db.ManufacturerRepository.Get().Any(m => m.Name == name && m.Id != id))
            return new OperationDetails(false, "A manufacturer with this name already exists", "");

        manufacturer.Name = name;
        db.ManufacturerRepository.Update(manufacturer);
        await db.SaveAsync();
        return new OperationDetails(true, "Manufacturer updated successfully", "");
    }

    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");

        var manufacturer = db.ManufacturerRepository.GetById(id);
        if (manufacturer == null) return new OperationDetails(false, "Manufacturer not found", "");

        db.ManufacturerRepository.Delete(manufacturer);
        await db.SaveAsync();
        return new OperationDetails(true, "Manufacturer deleted successfully", "");
    }
}