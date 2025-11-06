using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.Repositories;

public class OrderRepository(ApplicationDbContext db) : Repository<Order>(db), IOrderRepository;