using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Services;

public class CategoryService(IUnitOfWork db) : ICategoryService
{
    public List<Category> GetAllCategory()
    {
        var category = db.CategoryRepository.GetAll().ToList();
        return category;
    }

    public async Task<OperationDetails> AddCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return new OperationDetails(false, "Category name is required", "");
            
        if (db.CategoryRepository.Get().Any(c => c.Name == name))
            return new OperationDetails(false, "The same category already exists in database", "");

        db.CategoryRepository.Add(new Category { Name = name });
        await db.SaveAsync();
        return new OperationDetails(true, "Category added successfully", "");
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
        return new OperationDetails(true, "Category updated successfully", "");
    }

    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");
        
        var category = db.CategoryRepository.GetById(id);
        if (category == null) return new OperationDetails(false, "Category not found", "");

        db.CategoryRepository.Delete(category);
        await db.SaveAsync();
        return new OperationDetails(true, "Category deleted successfully", "");
    }
}