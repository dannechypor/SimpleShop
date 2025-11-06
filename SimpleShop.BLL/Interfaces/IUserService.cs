using Model.DTO;
using Shop.BLL.Infrastructure;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces;

public interface IUserService
{
    Task<OperationDetails> Create(UserDTO userDto);
    Task<bool> Authenticate(UserDTO userDto);
        
}