using Model.Entities;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail> , IOrderDetailRepository
    {
        public OrderDetailRepository(ApplicationDbContext db) : base (db)
        {
                
        }
    }
}
