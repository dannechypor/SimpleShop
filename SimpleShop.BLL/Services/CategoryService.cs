using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Services;

public class CategoryService(IUnitOfWork db, ILogger<CategoryService> logger, IMemoryCache cache) : ICategoryService
{
    private const string CacheKey = "AllCategories";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(30);

    public List<Category> GetAllCategory()
    {
        if (!cache.TryGetValue(CacheKey, out List<Category> categories))
        {
            categories = db.CategoryRepository.GetAll().ToList();
            cache.Set(CacheKey, categories, CacheExpiration);
            logger.LogDebug("Categories loaded from database and cached");
        }
        else
        {
            logger.LogDebug("Categories loaded from cache");
        }

        return categories;
    }

    public PagedResultDto<Category> GetAllCategoryPaged(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var allCategories = GetAllCategory();
        var totalCount = allCategories.Count;
        var items = allCategories
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PagedResultDto<Category>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<OperationDetails> AddCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            logger.LogWarning("AddCategory: Category name is empty");
            return new OperationDetails(false, "Category name is required", "");
        }

        if (db.CategoryRepository.Get().Any(c => c.Name == name))
        {
            logger.LogWarning("AddCategory: Category already exists. Name: {Name}", name);
            return new OperationDetails(false, "The same category already exists in database", "");
        }

        try
        {
            db.CategoryRepository.Add(new Category { Name = name });
            await db.SaveAsync();

            // Invalidate cache
            cache.Remove(CacheKey);
            logger.LogInformation("Category added successfully. Name: {Name}", name);
            return new OperationDetails(true, "Category added successfully", "");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding category. Name: {Name}", name);
            return new OperationDetails(false, "Error adding category", "");
        }
    }


    public async Task<OperationDetails> Update(int id, string name)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");
        if (string.IsNullOrWhiteSpace(name))
            return new OperationDetails(false, "Category name is required", "");

        var category = db.CategoryRepository.GetById(id);
        if (category == null) return new OperationDetails(false, "Category not found", "");

        // Check if another category with the same name exists
        if (db.CategoryRepository.Get().Any(c => c.Name == name && c.Id != id))
            return new OperationDetails(false, "A category with this name already exists", "");

        category.Name = name;
        db.CategoryRepository.Update(category);
        await db.SaveAsync();

        // Invalidate cache
        cache.Remove(CacheKey);
        logger.LogInformation("Category updated successfully. Id: {Id}, Name: {Name}", id, name);
        return new OperationDetails(true, "Category updated successfully", "");
    }

    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");

        var category = db.CategoryRepository.GetById(id);
        if (category == null) return new OperationDetails(false, "Category not found", "");

        db.CategoryRepository.Delete(category);
        await db.SaveAsync();

        // Invalidate cache
        cache.Remove(CacheKey);
        logger.LogInformation("Category deleted successfully. Id: {Id}, Name: {Name}", id, category.Name);
        return new OperationDetails(true, "Category deleted successfully", "");
    }
}