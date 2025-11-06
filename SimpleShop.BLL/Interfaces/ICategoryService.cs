using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.BLL.Infrastructure;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;

namespace SimpleShop.BLL.Interfaces;

public interface ICategoryService
{
    List<Category> GetAllCategory();
    PagedResultDto<Category> GetAllCategoryPaged(int pageNumber = 1, int pageSize = 10);
    Task<OperationDetails> AddCategory(string name);
    Task<OperationDetails> Update(int id, string name);
    Task<OperationDetails> Delete(int id);
}