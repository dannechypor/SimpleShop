using System.Threading.Tasks;
using Model.DTO;
using Shop.BLL.Infrastructure;

namespace Shop.BLL.Interfaces;

public interface IUserService
{
    Task<OperationDetails> Create(UserDto userDto);
    Task<bool> Authenticate(UserDto userDto);
}