using System.Linq;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using SimpleShop.DAL.Interfaces;

namespace SimpleShop.BLL.Services;

public class UserService : IUserService
{
    public UserService(IUnitOfWork uow)
    {
        Database = uow;
    }

    private IUnitOfWork Database { get; }

    public async Task<OperationDetails> Create(UserDto userDto)
    {
        var user = await Database.UserManager.FindByEmailAsync(userDto.Email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                Email = userDto.Email, UserName = userDto.Email, FirstName = userDto.FirstName,
                LastName = userDto.LastName, Address = userDto.Address, PhoneNumber = userDto.PhoneNumber
            };
            var result = await Database.UserManager.CreateAsync(user, userDto.Password);
            if (result.Errors.Any())
                return new OperationDetails(false, result.Errors.FirstOrDefault()?.ToString(), "");

            await Database.UserManager.AddToRoleAsync(user, userDto.Role);

            await Database.SaveAsync();

            return new OperationDetails(true, "Registration successfully completed!", "");
        }

        return new OperationDetails(false, "User with this login already exists", "Email");
    }

    public async Task<bool> Authenticate(UserDto userDto)
    {
        var user = await Database.UserManager.FindByEmailAsync(userDto.Email);
        if (user == null) return false;

        //var username = user.UserName;
        var email = user.Email;

        var auth = await Database.SignInManager.PasswordSignInAsync(email, userDto.Password, false,
            false);

        return auth.Succeeded;
    }
}