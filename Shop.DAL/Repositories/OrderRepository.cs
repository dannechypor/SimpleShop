using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories
{
    public class OrderRepository : Repository<Order> , IOrderRepository
    {
        public OrderRepository(ApplicationDbContext db) : base(db)
        {

        }
          
    }
}
