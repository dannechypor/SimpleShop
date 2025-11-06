using Model.Entities;
using Shop.DAL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.Model.Entities;

namespace Shop.DAL.Repositories;

public class OrderRepository(ApplicationDbContext db) : Repository<Order>(db), IOrderRepository;