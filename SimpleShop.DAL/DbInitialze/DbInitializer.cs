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
        await EnsureRoleAsync(db, "Admin");
        await EnsureRoleAsync(db, "User");

        var adminUser = await CreateUserIfNotExistsAsync(
            db,
            email: "admin@shop.local",
            password: "Admin_12345",
            firstName: "Admin",
            lastName: "User",
            address: "Main Street 1, Demo City",
            phoneNumber: "+10000000001");

        var customerUser = await CreateUserIfNotExistsAsync(
            db,
            email: "customer@shop.local",
            password: "Customer_12345",
            firstName: "Customer",
            lastName: "Demo",
            address: "Customer Avenue 5, Demo City",
            phoneNumber: "+10000000002");

        await EnsureUserInRoleAsync(db, adminUser, "Admin");
        await EnsureUserNotInRoleAsync(db, adminUser, "User");

        await EnsureUserInRoleAsync(db, customerUser, "User");
        await EnsureUserNotInRoleAsync(db, customerUser, "Admin");

        if (db.ProductRepository.GetAll().Any() ||
            db.CategoryRepository.GetAll().Any() ||
            db.ManufacturerRepository.GetAll().Any())
        {
            return;
        }

        var categories = new List<Category>
        {
            new() { Name = "Cabinets" },
            new() { Name = "Tables" },
            new() { Name = "Sofas" },
            new() { Name = "Chairs" }
        };
        db.CategoryRepository.AddRange(categories);

        var manufacturers = new List<Manufacturer>
        {
            new() { Name = "MebelStar" },
            new() { Name = "DochyloFactory" },
            new() { Name = "Mebelyer" },
            new() { Name = "Daniro" },
            new() { Name = "WoodCraft" },
            new() { Name = "SoftLine" },
            new() { Name = "NordicHome" },
            new() { Name = "UrbanLoft" }
        };
        db.ManufacturerRepository.AddRange(manufacturers);

        const string cabinetShort =
            "Minimal cabinets and sideboards for storing clothes, documents and everyday items. Clean design and reliable hardware.";

        const string cabinetLong =
            "A collection of cabinets and sideboards for living rooms, bedrooms and home offices. Practical internal layouts, durable hardware and finishes that are easy to clean. Different heights and widths so the pieces fit both compact apartments and larger interiors.";

        const string tableShort =
            "Functional tables for dining, work and study. Stable constructions and comfortable sizes.";

        const string tableLong =
            "Tables designed as the center of the room. The collection includes writing desks, dining tables and multi-purpose tables in various sizes. Built for everyday use and easy to combine with other furniture pieces.";

        const string sofaShort =
            "Comfortable sofas for everyday relaxation. Soft seats, supportive armrests and a modern look.";

        const string sofaLong =
            "Soft sofas designed for cozy living rooms. Sizes range from compact models for small spaces to large sofas for spacious interiors. Durable upholstery, ergonomic seating and simple maintenance.";

        const string chairShort =
            "Comfortable chairs for dining areas, workspaces and living rooms. Stable, lightweight and stylish.";

        const string chairLong =
            "A collection of chairs in different shapes: from simple dining chairs to more ergonomic models for long sitting. Stable bases, supportive backs and seats that work well for everyday use.";

        var cabinetImages = new[]
        {
            "https://www.svgrepo.com/download/380355/cabinet-closet-cupboard-furniture-wardrobe.svg",
            "https://www.svgrepo.com/download/421334/cabinet-closet-cupboard.svg",
            "https://www.svgrepo.com/download/424289/cabinet-furniture-home-24.svg"
        };

        var tableImages = new[]
        {
            "https://www.svgrepo.com/download/258992/table-furniture-and-household.svg",
            "https://www.svgrepo.com/download/258988/table-furniture.svg",
            "https://www.svgrepo.com/download/217058/table-furniture-and-household.svg"
        };

        var sofaImages = new[]
        {
            "https://www.svgrepo.com/download/292743/sofa-furniture-and-household.svg",
            "https://www.svgrepo.com/download/66889/sofa.svg",
            "https://www.svgrepo.com/download/216934/sofa.svg"
        };

        var chairImages = new[]
        {
            "https://www.svgrepo.com/download/223866/office-chair-furniture-and-household.svg",
            "https://www.svgrepo.com/download/92933/office-chair.svg",
            "https://www.svgrepo.com/download/380364/chair-contemporary-decor-furniture-home-3.svg"
        };

        const int itemsPerCategory = 12;

        var products = new List<Product>();

        products.AddRange(CreateProducts(
            baseName: "Cabinet",
            categoryId: 1,
            basePrice: 3500m,
            shortDescription: cabinetShort,
            longDescription: cabinetLong,
            images: cabinetImages,
            manufacturerIds: new[] { 1, 4, 5, 7 },
            count: itemsPerCategory));

        products.AddRange(CreateProducts(
            baseName: "Table",
            categoryId: 2,
            basePrice: 2800m,
            shortDescription: tableShort,
            longDescription: tableLong,
            images: tableImages,
            manufacturerIds: new[] { 2, 5, 8 },
            count: itemsPerCategory));

        products.AddRange(CreateProducts(
            baseName: "Sofa",
            categoryId: 3,
            basePrice: 5500m,
            shortDescription: sofaShort,
            longDescription: sofaLong,
            images: sofaImages,
            manufacturerIds: new[] { 3, 6, 7 },
            count: itemsPerCategory));

        products.AddRange(CreateProducts(
            baseName: "Chair",
            categoryId: 4,
            basePrice: 1600m,
            shortDescription: chairShort,
            longDescription: chairLong,
            images: chairImages,
            manufacturerIds: new[] { 2, 4, 6, 8 },
            count: itemsPerCategory));

        db.ProductRepository.AddRange(products);

        await db.SaveAsync();
    }

    private static async Task EnsureRoleAsync(IUnitOfWork db, string roleName)
    {
        if (!await db.RoleManager.RoleExistsAsync(roleName))
        {
            await db.RoleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    private static async Task<ApplicationUser> CreateUserIfNotExistsAsync(
        IUnitOfWork db,
        string email,
        string password,
        string firstName,
        string lastName,
        string address,
        string phoneNumber)
    {
        var user = await db.UserManager.FindByEmailAsync(email);
        if (user != null)
            return user;

        user = new ApplicationUser
        {
            Email = email,
            UserName = email,
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            PhoneNumber = phoneNumber
        };

        var result = await db.UserManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create seed user {email}: {errors}");
        }

        return user;
    }

    private static async Task EnsureUserInRoleAsync(IUnitOfWork db, ApplicationUser user, string role)
    {
        if (!await db.UserManager.IsInRoleAsync(user, role))
        {
            await db.UserManager.AddToRoleAsync(user, role);
        }
    }

    private static async Task EnsureUserNotInRoleAsync(IUnitOfWork db, ApplicationUser user, string role)
    {
        if (await db.UserManager.IsInRoleAsync(user, role))
        {
            await db.UserManager.RemoveFromRoleAsync(user, role);
        }
    }

    private static IEnumerable<Product> CreateProducts(
        string baseName,
        int categoryId,
        decimal basePrice,
        string shortDescription,
        string longDescription,
        string[] images,
        int[] manufacturerIds,
        int count)
    {
        var list = new List<Product>();

        for (var i = 0; i < count; i++)
        {
            var imgIndex = i % images.Length;
            var manufacturerId = manufacturerIds[i % manufacturerIds.Length];

            list.Add(new Product
            {
                Name = $"{baseName} {i + 1}",
                Price = basePrice + i * 50,
                ImageUrl_1 = images[imgIndex],
                ImageUrl_2 = images[(imgIndex + 1) % images.Length],
                ImageUrl_3 = images[(imgIndex + 2) % images.Length],
                Date = DateTime.UtcNow,
                Hight = "80",
                Width = "120",
                Deepth = "60",
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                CategoryId = categoryId,
                ManufacturerId = manufacturerId
            });
        }

        return list;
    }
}