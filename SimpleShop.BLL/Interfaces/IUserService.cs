using Model.DTO;
using Shop.BLL.Infrastructure;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces;

public interface IUserService
{
    Task<OperationDetails> Create(UserDto userDto);
    Task<bool> Authenticate(UserDto userDto);
        
}