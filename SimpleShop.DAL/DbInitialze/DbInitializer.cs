using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Model.Entities;
using Shop.DAL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace SimpleShop.DAL.DbInitialze;

public static class DbInitializer
{
    public static async Task Seed(IUnitOfWork db)
    {
        // ---------- Identity data ----------
        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await db.RoleManager.RoleExistsAsync(role))
            {
                await db.RoleManager.CreateAsync(new IdentityRole(role));
            }
        }

        async Task<ApplicationUser> EnsureUserAsync(
            string email,
            string password,
            string firstName,
            string lastName,
            string address,
            string phone)
        {
            var user = await db.UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }

            user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phone
            };

            var createResult = await db.UserManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                throw new InvalidOperationException(
                    $"Failed to create seed user {email}: " +
                    string.Join(", ", createResult.Errors.Select(e => e.Description)));
            }

            return user;
        }

        var adminUser = await EnsureUserAsync(
            email: "admin@simpleshop.local",
            password: "Admin_12345",
            firstName: "Alex",
            lastName: "Admin",
            address: "Main Street 1, Demo City",
            phone: "+10000000001");

        var regularUser = await EnsureUserAsync(
            email: "customer@simpleshop.local",
            password: "User_12345",
            firstName: "Jamie",
            lastName: "Customer",
            address: "Oak Avenue 5, Demo City",
            phone: "+10000000002");

        if (!await db.UserManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await db.UserManager.AddToRoleAsync(adminUser, "Admin");
        }

        if (!await db.UserManager.IsInRoleAsync(regularUser, "User"))
        {
            await db.UserManager.AddToRoleAsync(regularUser, "User");
        }

        // ---------- Catalog data ----------
        if (db.CategoryRepository.GetAll().Any() ||
            db.ManufacturerRepository.GetAll().Any() ||
            db.ProductRepository.GetAll().Any())
        {
            // Already seeded
            return;
        }

        // Shared descriptions (no duplication per product)
        const string defaultLongDescription =
            "Clean, modern furniture built from solid materials and durable fabrics. " +
            "Every piece is designed for everyday use in contemporary homes with " +
            "soft-close hardware, rounded edges and easy-to-clean finishes.";

        const string defaultShortDescription =
            "Modern, durable furniture ideal for contemporary living and dining spaces.";

        var now = DateTime.UtcNow;

        // Image URLs – free stock photos from Unsplash (furniture, interiors)
        // Cabinets / wardrobes
        const string cabinetImage1 = "https://unsplash.com/photos/a-room-filled-with-lots-of-white-cabinets-OosDQ87hK6I";
        const string cabinetImage2 = "https://unsplash.com/photos/a-room-with-a-wooden-floor-and-white-closets-YbcX1Sbn5Qk";
        const string cabinetImage3 = "https://unsplash.com/photos/modern-bedroom-with-a-sleek-wardrobe-design-JnAqOVTHx7w";

        // Tables
        const string tableImage1 = "https://unsplash.com/photos/modern-dining-table-with-upholstered-chairs-PXfUGQXaHNQ";
        const string tableImage2 = "https://unsplash.com/photos/dining-table-with-chairs-and-a-plant-G1ZMhQZDHOQ";
        const string tableImage3 = "https://unsplash.com/photos/dining-table-with-six-upholstered-chairs-in-a-bright-room-SrbklO9p8gI";

        // Sofas
        const string sofaImage1 = "https://unsplash.com/photos/a-living-room-with-a-couch-and-a-table-4Q9aOJF-6vI";
        const string sofaImage2 = "https://unsplash.com/photos/living-room-with-brown-sofa-and-glass-window-4_jQL4JCS98";
        const string sofaImage3 = "https://unsplash.com/photos/a-living-room-with-a-large-green-couch-VZ2z8ozzy10";

        // Chairs
        const string chairImage1 = "https://unsplash.com/photos/brown-wooden-chairs-v4sxFK-6k8U";
        const string chairImage2 = "https://unsplash.com/photos/a-wooden-chair-sitting-on-top-of-a-wooden-floor-RCWIK0fsETc";
        const string chairImage3 = "https://unsplash.com/photos/white-and-brown-wooden-chair-qdq8eDgRelA";

        // Categories
        var categories = new[]
        {
            new Category { Name = "Cabinets & Storage" },   // 0
            new Category { Name = "Tables" },               // 1
            new Category { Name = "Sofas" },                // 2
            new Category { Name = "Chairs" }                // 3
        };

        db.CategoryRepository.AddRange(categories);

        // Manufacturers
        var manufacturers = new[]
        {
            new Manufacturer { Name = "Nordic Home" },      // 0
            new Manufacturer { Name = "Urban Loft Studio" },// 1
            new Manufacturer { Name = "ComfortLine" },      // 2
            new Manufacturer { Name = "Oak & Steel" }       // 3
        };

        db.ManufacturerRepository.AddRange(manufacturers);

        Category Cat(string name) =>
            categories.Single(c => c.Name == name);

        Manufacturer M(string name) =>
            manufacturers.Single(m => m.Name == name);

        Product CreateProduct(
            string name,
            decimal price,
            string image1,
            string image2,
            string image3,
            string hight,
            string width,
            string depth,
            Category category,
            Manufacturer manufacturer)
        {
            return new Product
            {
                Name = name,
                Price = price,
                ImageUrl_1 = image1,
                ImageUrl_2 = image2,
                ImageUrl_3 = image3,
                Date = now,
                Hight = hight,
                Width = width,
                Deepth = depth,
                LongDescription = defaultLongDescription,
                ShortDescription = defaultShortDescription,
                Category = category,
                Manufacturer = manufacturer
            };
        }

        var products = new List<Product>
        {
            // Cabinets & Storage
            CreateProduct(
                name: "White Modular Wardrobe",
                price: 4200,
                image1: cabinetImage1,
                image2: cabinetImage2,
                image3: cabinetImage3,
                hight: "210",
                width: "160",
                depth: "60",
                category: Cat("Cabinets & Storage"),
                manufacturer: M("Nordic Home")),

            CreateProduct(
                name: "Compact Hallway Cabinet",
                price: 2800,
                image1: cabinetImage2,
                image2: cabinetImage1,
                image3: cabinetImage3,
                hight: "190",
                width: "90",
                depth: "40",
                category: Cat("Cabinets & Storage"),
                manufacturer: M("Urban Loft Studio")),

            CreateProduct(
                name: "Sliding Door Wardrobe",
                price: 5100,
                image1: cabinetImage3,
                image2: cabinetImage1,
                image3: cabinetImage2,
                hight: "220",
                width: "180",
                depth: "65",
                category: Cat("Cabinets & Storage"),
                manufacturer: M("Oak & Steel")),

            // Tables
            CreateProduct(
                name: "Oslo Oak Dining Table",
                price: 3600,
                image1: tableImage1,
                image2: tableImage2,
                image3: tableImage3,
                hight: "76",
                width: "180",
                depth: "90",
                category: Cat("Tables"),
                manufacturer: M("Oak & Steel")),

            CreateProduct(
                name: "Mila Round Dining Table",
                price: 3100,
                image1: tableImage2,
                image2: tableImage1,
                image3: tableImage3,
                hight: "75",
                width: "130",
                depth: "130",
                category: Cat("Tables"),
                manufacturer: M("Nordic Home")),

            CreateProduct(
                name: "Luna Coffee Table Set",
                price: 1900,
                image1: tableImage3,
                image2: tableImage2,
                image3: tableImage1,
                hight: "45",
                width: "120",
                depth: "60",
                category: Cat("Tables"),
                manufacturer: M("Urban Loft Studio")),

            // Sofas
            CreateProduct(
                name: "Cloud 3-Seater Sofa",
                price: 5200,
                image1: sofaImage1,
                image2: sofaImage2,
                image3: sofaImage3,
                hight: "90",
                width: "220",
                depth: "95",
                category: Cat("Sofas"),
                manufacturer: M("ComfortLine")),

            CreateProduct(
                name: "Nova Corner Sofa",
                price: 7800,
                image1: sofaImage2,
                image2: sofaImage1,
                image3: sofaImage3,
                hight: "92",
                width: "260",
                depth: "190",
                category: Cat("Sofas"),
                manufacturer: M("ComfortLine")),

            CreateProduct(
                name: "Haven Compact Sofa",
                price: 4300,
                image1: sofaImage3,
                image2: sofaImage1,
                image3: sofaImage2,
                hight: "88",
                width: "190",
                depth: "90",
                category: Cat("Sofas"),
                manufacturer: M("Nordic Home")),

            // Chairs
            CreateProduct(
                name: "Ivy Lounge Chair",
                price: 1700,
                image1: chairImage1,
                image2: chairImage2,
                image3: chairImage3,
                hight: "85",
                width: "75",
                depth: "80",
                category: Cat("Chairs"),
                manufacturer: M("Urban Loft Studio")),

            CreateProduct(
                name: "Orbit Office Chair",
                price: 2100,
                image1: chairImage2,
                image2: chairImage1,
                image3: chairImage3,
                hight: "110",
                width: "65",
                depth: "70",
                category: Cat("Chairs"),
                manufacturer: M("ComfortLine")),

            CreateProduct(
                name: "Nordic Dining Chair",
                price: 950,
                image1: chairImage3,
                image2: chairImage1,
                image3: chairImage2,
                hight: "82",
                width: "48",
                depth: "55",
                category: Cat("Chairs"),
                manufacturer: M("Oak & Steel"))
        };

        db.ProductRepository.AddRange(products);

        await db.SaveAsync();
    }
}