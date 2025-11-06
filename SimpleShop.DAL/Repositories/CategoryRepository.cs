using Model.Entities;
using Shop.DAL.EF;
using Shop.DAL.Interfaces;

namespace Shop.DAL.Repositories;

public class CategoryRepository(ApplicationDbContext db) : Repository<Category>(db), ICategoryRepository;