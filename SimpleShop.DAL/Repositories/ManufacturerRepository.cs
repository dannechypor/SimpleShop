using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.Repositories;

public class ManufacturerRepository(ApplicationDbContext db) : Repository<Manufacturer>(db), IManufacturerRepository;