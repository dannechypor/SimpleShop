using Model.Entities;
using Shop.DAL.EF;
using Shop.DAL.Interfaces;

namespace Shop.DAL.Repositories;

public class ManufacturerRepository(ApplicationDbContext db) : Repository<Manufacturer>(db), IManufacturerRepository;