using Model.Entities;
using Shop.DAL.Interfaces;
using Shop.DAL.EF;

namespace Shop.DAL.Repositories;

public class OrderDetailRepository(ApplicationDbContext db) : Repository<OrderDetail>(db), IOrderDetailRepository;