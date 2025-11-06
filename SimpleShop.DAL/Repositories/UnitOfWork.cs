using Microsoft.AspNetCore.Identity;
using Model.Entities;
using Shop.DAL.Interfaces;
using Shop.DAL.EF;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.DAL.Repositories;

public sealed class UnitOfWork(
    ApplicationDbContext dbContext,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IReviewRepository reviewRepository,
    IManufacturerRepository manufacturerRepository,
    IOrderRepository orderRepository,
    IOrderDetailRepository orderDetailRepository,
    IWishListRepository wishListRepository)
    : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext 
        ?? throw new ArgumentNullException(nameof(dbContext));

    public UserManager<ApplicationUser> UserManager { get; } =
        userManager ?? throw new ArgumentNullException(nameof(userManager));

    public RoleManager<IdentityRole> RoleManager { get; } =
        roleManager ?? throw new ArgumentNullException(nameof(roleManager));

    public SignInManager<ApplicationUser> SignInManager { get; } =
        signInManager ?? throw new ArgumentNullException(nameof(signInManager));

    public IProductRepository ProductRepository { get; } =
        productRepository ?? throw new ArgumentNullException(nameof(productRepository));

    public ICategoryRepository CategoryRepository { get; } =
        categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));

    public IReviewRepository ReviewRepository { get; } =
        reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));

    public IManufacturerRepository ManufacturerRepository { get; } =
        manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));

    public IOrderRepository OrderRepository { get; } =
        orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));

    public IOrderDetailRepository OrderDetailRepository { get; } =
        orderDetailRepository ?? throw new ArgumentNullException(nameof(orderDetailRepository));

    public IWishListRepository WishListRepository { get; } =
        wishListRepository ?? throw new ArgumentNullException(nameof(wishListRepository));

    public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}