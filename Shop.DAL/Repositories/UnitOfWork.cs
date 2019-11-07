using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private ApplicationDbContext Database;
        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        private ICategoryRepository categoryRepository;
        private IProductRepository productRepository;
        private IReviewRepository reviewRepository;
        private IManufacturerRepositroy manufacturerRepositroy;
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private IWishListPrepository wishListPrepository;
        
       
            
        public UnitOfWork(ApplicationDbContext db,SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)                        
        {
            Database = db;
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;                                    
        }


        public IProductRepository ProductRepository =>
            productRepository ?? (productRepository = new ProductRepository(Database));

        public ICategoryRepository CategoryRepository =>
            categoryRepository ?? (categoryRepository = new CategoryRepository(Database));

        public IReviewRepository ReviewRepository =>
            reviewRepository ?? (reviewRepository = new ReviewRepository(Database));

        public IManufacturerRepositroy ManufacturerRepositroy =>
            manufacturerRepositroy ?? (manufacturerRepositroy = new ManufacturerRepository(Database));

        public IOrderRepository OrderRepository =>
            orderRepository ?? (orderRepository = new OrderRepository(Database));

        public IOrderDetailRepository OrderDetailRepository =>
            orderDetailRepository ?? (orderDetailRepository = new OrderDetailRepository(Database));

        public IWishListPrepository WishListPrepository =>
           wishListPrepository ?? (wishListPrepository = new WishListRepository(Database));



        public async Task SaveAsync()
        {
            await Database.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    
                }
                this.disposed = true;
            }
        }
    }
}
