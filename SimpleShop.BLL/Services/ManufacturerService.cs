using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Shop.BLL.Infrastructure;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Services;

public class ManufacturerService(IUnitOfWork db, ILogger<ManufacturerService> logger, IMemoryCache cache)
    : IManufacturerService
{
    private const string CacheKey = "AllManufacturers";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

    public List<Manufacturer> GetAllManufacturer()
    {
        if (!cache.TryGetValue(CacheKey, out List<Manufacturer> manufacturers))
        {
            manufacturers = db.ManufacturerRepository.GetAll().ToList();
            cache.Set(CacheKey, manufacturers, CacheExpiration);
            logger.LogDebug("Manufacturers loaded from database and cached");
        }
        else
        {
            logger.LogDebug("Manufacturers loaded from cache");
        }

        return manufacturers;
    }

    public PagedResultDto<Manufacturer> GetAllManufacturerPaged(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var allManufacturers = GetAllManufacturer();
        var totalCount = allManufacturers.Count;
        var items = allManufacturers
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResultDto<Manufacturer>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<OperationDetails> AddManufacturer(string Name)
    {
        if (string.IsNullOrWhiteSpace(Name))
            return new OperationDetails(false, "Manufacturer name is required", "");

        if (db.ManufacturerRepository.Get().Any(c => c.Name == Name))
            return new OperationDetails(false, "The same manufacturer already exists in database", "");

        db.ManufacturerRepository.Add(new Manufacturer { Name = Name });
        await db.SaveAsync();

        // Invalidate cache
        cache.Remove(CacheKey);
        logger.LogInformation("Manufacturer added successfully. Name: {Name}", Name);
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

        // Invalidate cache
        cache.Remove(CacheKey);
        logger.LogInformation("Manufacturer updated successfully. Id: {Id}, Name: {Name}", id, name);
        return new OperationDetails(true, "Manufacturer updated successfully", "");
    }

    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");

        var manufacturer = db.ManufacturerRepository.GetById(id);
        if (manufacturer == null) return new OperationDetails(false, "Manufacturer not found", "");

        db.ManufacturerRepository.Delete(manufacturer);
        await db.SaveAsync();

        // Invalidate cache
        cache.Remove(CacheKey);
        logger.LogInformation("Manufacturer deleted successfully. Id: {Id}, Name: {Name}", id, manufacturer.Name);
        return new OperationDetails(true, "Manufacturer deleted successfully", "");
    }
}