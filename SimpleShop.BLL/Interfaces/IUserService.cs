using System.Threading.Tasks;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.Model.DTO;

namespace SimpleShop.BLL.Interfaces;

public interface IUserService
{
    Task<OperationDetails> Create(UserDto userDto);
    Task<bool> Authenticate(UserDto userDto);
}