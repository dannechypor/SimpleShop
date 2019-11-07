using Model.Entities;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL.Repositories
{
    public class ManufacturerRepository : Repository<Manufacturer> , IManufacturerRepositroy
    {
        public ManufacturerRepository(ApplicationDbContext db) : base(db)
        {

        }
    }
}
