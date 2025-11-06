using Model.Entities;
using Shop.DAL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.DAL.Repositories;
using SimpleShop.Model.Entities;

namespace Shop.DAL.Repositories;

public class ManufacturerRepository(ApplicationDbContext db) : Repository<Manufacturer>(db), IManufacturerRepository;