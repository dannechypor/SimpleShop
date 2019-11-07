using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface ICategoryService : IDisposable
    {
        List<Category> GetAllCategory();
        Task<OperationDetails> AddCategory( string Name);
        Task<OperationDetails> Delete(int id);
    }
}
