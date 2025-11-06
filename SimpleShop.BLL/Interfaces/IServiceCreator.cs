using Microsoft.EntityFrameworkCore;
using SimpleShop.DAL.EF;

namespace SimpleShop.BLL.Interfaces;

public interface IServiceCreator
{
    IUserService CreateUserService(DbContextOptions<ApplicationDbContext> options);
}