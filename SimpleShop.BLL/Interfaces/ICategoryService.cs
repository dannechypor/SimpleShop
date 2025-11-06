using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Entities;
using Shop.BLL.Infrastructure;

namespace Shop.BLL.Interfaces;

public interface ICategoryService
{
    List<Category> GetAllCategory();
    Task<OperationDetails> AddCategory(string name);
    Task<OperationDetails> Delete(int id);
}