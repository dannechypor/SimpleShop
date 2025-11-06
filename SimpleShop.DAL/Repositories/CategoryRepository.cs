using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.Repositories;

public class CategoryRepository(ApplicationDbContext db) : Repository<Category>(db), ICategoryRepository;