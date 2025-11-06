using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using Shop.DAL;
using Shop.DAL.DbInitialze;
using Shop.DAL.EF;
using Shop.DAL.Interfaces;
using Shop.DAL.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("SimpleShop.DAL")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1440);

    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddMvc();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IUserService, UserService>();          
builder.Services.AddTransient<ICategoryService, CategoryService>();                     
builder.Services.AddTransient<ICatalogService, CatalogeService>();                     
builder.Services.AddTransient<IProductService, ProductService>();      
builder.Services.AddTransient<IReviewService, ReviewService>();      
builder.Services.AddTransient<IOrderService, OrderService>();      
builder.Services.AddTransient<IManufacturerService, ManufacturerService>();      
builder.Services.AddTransient<IWishListService, WishListService>();      
builder.Services.AddScoped<IShoppingCartService>(ShoppingCartService.GetCart);



builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();         
builder.Services.AddTransient<IOrderRepository, OrderRepository>();         
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
          

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddMemoryCache();

var app = builder.Build();

var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetService<IUnitOfWork>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();