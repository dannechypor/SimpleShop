using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;

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
        if (db.CategoryRepository.Get().Any(c => c.Name == name))
            return new OperationDetails(false, "The same category is already exist in database", "");

        db.CategoryRepository.Add(new Category { Name = name });
        await db.SaveAsync();
        return new OperationDetails(true, "Ok", "");
    }


    public async Task<OperationDetails> Delete(int id)
    {
        if (id == 0) return new OperationDetails(false, "Id field is '0'", "");
        var category = db.CategoryRepository.GetById(id);

        db.CategoryRepository.Delete(category);
        await db.SaveAsync();
        return new OperationDetails(true, "", "");
    }
}