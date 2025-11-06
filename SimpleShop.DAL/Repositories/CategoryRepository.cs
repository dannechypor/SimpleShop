using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.DAL.Repositories;
using SimpleShop.Model.Entities;

namespace Shop.DAL.Repositories;

public class CategoryRepository(ApplicationDbContext db) : Repository<Category>(db), ICategoryRepository;