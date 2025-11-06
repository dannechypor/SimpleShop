using Model.Entities;
using Shop.DAL.Interfaces;
using Shop.DAL.EF;

namespace Shop.DAL.Repositories;

public class OrderRepository(ApplicationDbContext db) : Repository<Order>(db), IOrderRepository;