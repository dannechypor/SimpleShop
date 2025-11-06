using Microsoft.EntityFrameworkCore;
using Shop.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.DAL.EF;

namespace Shop.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(DbContextOptions<ApplicationDbContext> options);
    }
}
