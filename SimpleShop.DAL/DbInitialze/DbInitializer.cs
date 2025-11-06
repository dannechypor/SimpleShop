using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Model.Entities;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.DbInitialze;

public static class DbInitializer
{
    public static async Task Seed(IUnitOfWork db)
    {
        // ---------- ROLES ----------
        var roles = new[] { "Admin", "User" };
        foreach (var roleName in roles)
        {
            if (!await db.RoleManager.RoleExistsAsync(roleName))
            {
                await db.RoleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // ---------- USERS ----------
        async Task<ApplicationUser> EnsureUserAsync(
            string email,
            string firstName,
            string lastName,
            string address,
            string phone,
            string password,
            params string[] userRoles)
        {
            var user = await db.UserManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = email,
                    UserName = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = address,
                    PhoneNumber = phone
                };

                var result = await db.UserManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }

            foreach (var r in userRoles)
            {
                if (!await db.UserManager.IsInRoleAsync(user, r))
                {
                    await db.UserManager.AddToRoleAsync(user, r);
                }
            }

            return user;
        }

        await EnsureUserAsync(
            email: "admin@admin.com",
            firstName: "Dan",
            lastName: "Nechypor",
            address: "Chernivtsy, 14",
            phone: "+380936581050",
            password: "admin12345",
            userRoles: new[] { "Admin" });

        await EnsureUserAsync(
            email: "user@user.com",
            firstName: "Dochylo",
            lastName: "Nazar",
            address: "Chernivtsy, 19",
            phone: "+380664703735",
            password: "user12345",
            userRoles: new[] { "User" });

        if (db.CategoryRepository.GetAll().Any() || db.ProductRepository.GetAll().Any())
        {
            return;
        }

        // ---------- CATEGORIES ----------
        var categories = new List<Category>
        {
            new() { Name = "Cabinets" }, // 1
            new() { Name = "Tables"   }, // 2
            new() { Name = "Sofas"    }, // 3
            new() { Name = "Chairs"   }  // 4
        };
        db.CategoryRepository.AddRange(categories);

        // ---------- MANUFACTURERS ----------
        var manufacturers = new List<Manufacturer>
        {
            new() { Name = "Nordic Home" },          // 1
            new() { Name = "Urban Loft Studio" },    // 2
            new() { Name = "ComfortLine" },          // 3
            new() { Name = "Oak & Steel" },          // 4
            new() { Name = "Casa Viva" },            // 5
            new() { Name = "GreenLeaf Interiors" },  // 6
            new() { Name = "ClassicWood" },          // 7
            new() { Name = "Skyline Furniture Co." } // 8
        };
        db.ManufacturerRepository.AddRange(manufacturers);

        // ---------- SHARED DESCRIPTIONS ----------
        const string longDescription =
            "Create a cozy, modern home with our curated furniture collection. " +
            "High-quality materials, practical dimensions and neutral colors make these pieces " +
            "easy to combine in any interior. Designed for everyday comfort, easy cleaning and long-term use.";

        const string shortDescription =
            "Modern, functional furniture made from durable materials. Perfect for everyday use in a family home or apartment.";

        // ---------- IMAGE URLS (DIRECT .JPEG/.PNG) ----------

        // Cabinets / storage
        var cabinetImages = new[]
        {
            "https://images.pexels.com/photos/803908/pexels-photo-803908.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
            "https://images.pexels.com/photos/2935956/pexels-photo-2935956.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500",
            "https://images.pexels.com/photos/276724/pexels-photo-276724.jpeg"
        };

        // Dining / tables
        var tableImages = new[]
        {
            "https://images.pexels.com/photos/3773583/pexels-photo-3773583.png?auto=compress&cs=tinysrgb&dpr=3&h=750&w=1260",
            "https://cdn.pixabay.com/photo/2014/10/02/02/33/dining-room-469298__340.jpg",
            "https://images.pexels.com/photos/6184427/pexels-photo-6184427.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940"
        };

        // Sofas / living room
        var sofaImages = new[]
        {
            "https://images.pexels.com/photos/276583/pexels-photo-276583.jpeg",
            "https://images.pexels.com/photos/298842/pexels-photo-298842.jpeg",
            "https://images.pexels.com/photos/584399/living-room-couch-interior-room-584399.jpeg?h=350&dpr=2&auto=compress&cs=tinysrgb"
        };

        // Chairs / seating
        var chairImages = new[]
        {
            "https://images.pexels.com/photos/6184427/pexels-photo-6184427.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
            "https://images.pexels.com/photos/2373201/pexels-photo-2373201.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=650&w=940",
            "https://cdn.pixabay.com/photo/2017/09/09/18/25/living-room-2732939_960_720.jpg"
        };

        // ---------- PRODUCT GENERATOR ----------
        Product CreateProduct(
            string baseName,
            int index,
            int categoryId,
            int manufacturerId,
            string imageUrl,
            string height,
            string width,
            string depth,
            decimal basePrice,
            decimal priceStep)
        {
            return new Product
            {
                Name = $"{baseName} {index}",
                Price = basePrice + priceStep * (index - 1),
                ImageUrl_1 = imageUrl,
                ImageUrl_2 = imageUrl,
                ImageUrl_3 = imageUrl,
                Date = DateTime.UtcNow,
                Hight = height,
                Width = width,
                Deepth = depth,
                LongDescription = longDescription,
                ShortDescription = shortDescription,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId
            };
        }

        IEnumerable<Product> GenerateProducts(
            string baseName,
            int categoryId,
            int count,
            int[] manufacturerIds,
            string[] imageUrls,
            decimal basePrice,
            decimal priceStep,
            string height,
            string width,
            string depth)
        {
            var list = new List<Product>();

            for (var i = 1; i <= count; i++)
            {
                var manufacturerId = manufacturerIds[(i - 1) % manufacturerIds.Length];
                var imageUrl = imageUrls[(i - 1) % imageUrls.Length];

                list.Add(CreateProduct(
                    baseName: baseName,
                    index: i,
                    categoryId: categoryId,
                    manufacturerId: manufacturerId,
                    imageUrl: imageUrl,
                    height: height,
                    width: width,
                    depth: depth,
                    basePrice: basePrice,
                    priceStep: priceStep));
            }

            return list;
        }

        // ---------- PRODUCTS ----------
        var products = new List<Product>();

        // Cabinets
        products.AddRange(GenerateProducts(
            baseName: "Oslo Oak Cabinet",
            categoryId: 1,
            count: 12,
            manufacturerIds: new[] { 1, 4, 7 },
            imageUrls: cabinetImages,
            basePrice: 3200m,
            priceStep: 150m,
            height: "210",
            width: "120",
            depth: "60"));

        // Tables
        products.AddRange(GenerateProducts(
            baseName: "Loft Dining Table",
            categoryId: 2,
            count: 12,
            manufacturerIds: new[] { 2, 4, 7 },
            imageUrls: tableImages,
            basePrice: 2800m,
            priceStep: 120m,
            height: "75",
            width: "160",
            depth: "90"));

        // Sofas
        products.AddRange(GenerateProducts(
            baseName: "Cloud Comfort Sofa",
            categoryId: 3,
            count: 12,
            manufacturerIds: new[] { 3, 5, 6 },
            imageUrls: sofaImages,
            basePrice: 4500m,
            priceStep: 200m,
            height: "90",
            width: "230",
            depth: "100"));

        // Chairs
        products.AddRange(GenerateProducts(
            baseName: "Nordic Lounge Chair",
            categoryId: 4,
            count: 12,
            manufacturerIds: new[] { 2, 4, 8 },
            imageUrls: chairImages,
            basePrice: 900m,
            priceStep: 60m,
            height: "85",
            width: "55",
            depth: "60"));

        db.ProductRepository.AddRange(products);

        await db.SaveAsync();
    }
}