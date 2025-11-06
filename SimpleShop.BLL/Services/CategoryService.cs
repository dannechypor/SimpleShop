using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
   public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork Database;


        public CategoryService(IUnitOfWork db)
        {
            Database = db;
        }

        public List<Category> GetAllCategory()
        {
            var category = Database.CategoryRepository.GetAll().ToList();
            return category;
        }

        public async Task<OperationDetails> AddCategory(/*CategoryDTO categoryDto*/string Name)
        {
            if (Database.CategoryRepository.Get().Any(c => c.Name == Name))
            {
                return new OperationDetails(false, "The same category is already exist in database", "");
            }

            Database.CategoryRepository.Add(new Category { Name = Name });
            await Database.SaveAsync();
            return new OperationDetails(true, "Ok", "");
        }


        public async Task<OperationDetails> Delete(int id)
        {
            if (id == 0)
            {
                return new OperationDetails(false, "Id field is '0'", "");
            }
            Category category = Database.CategoryRepository.GetById(id);
            if (category == null)
            {
                return new OperationDetails(false, "Not found", "");
            }

            Database.CategoryRepository.Delete(category);
            await Database.SaveAsync();
            return new OperationDetails(true, "", "");
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
