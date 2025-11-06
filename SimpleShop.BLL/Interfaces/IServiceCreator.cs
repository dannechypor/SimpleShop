using Microsoft.EntityFrameworkCore;
using Shop.BLL.Interfaces;
using SimpleShop.DAL.EF;

namespace SimpleShop.BLL.Interfaces;

public interface IServiceCreator
{
    IUserService CreateUserService(DbContextOptions<ApplicationDbContext> options);
}