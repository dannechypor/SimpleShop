using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Model.Entities;
using Shop.DAL.Interfaces;

namespace SimpleShop.DAL.Interfaces;

public interface IUnitOfWork
{
    UserManager<ApplicationUser> UserManager { get; }
    RoleManager<IdentityRole> RoleManager { get; }
    SignInManager<ApplicationUser> SignInManager { get; }
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IManufacturerRepository ManufacturerRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderDetailRepository OrderDetailRepository { get; }
    IWishListRepository WishListRepository { get; }
        
    Task<int> SaveAsync(CancellationToken cancellationToken = default);
}