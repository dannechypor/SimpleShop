using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { Database.Migrate();}



        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }       
        public DbSet<Manufacturer> Manufacturers { get; set; }       
        public DbSet<Review> Review { get; set; }       
        public DbSet<WhishListItem> WhishListItems { get; set; }       
        public DbSet<Order> Orders { get; set; }       
         
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ApplicationUser> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder
        .Entity<WhishListItem>()
        .HasKey(x => new { x.ProductId, x.UserId });
            modelBuilder
       .Entity<Order>()
       .Property(e => e.Status)
       .HasConversion(
           v => v.ToString(),
           v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));

        }
    }
}



