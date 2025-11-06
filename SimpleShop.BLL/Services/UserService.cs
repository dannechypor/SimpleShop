using Microsoft.AspNetCore.Identity;
using Model.DTO;
using Model.Entities;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;
using Model.Models;
using Shop.BLL.Services;


namespace Shop.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
       

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email, FirstName = userDto.FirstName, LastName = userDto.LastName, Address = userDto.Address,  PhoneNumber = userDto.PhoneNumber};
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault().ToString(), "");

                await Database.UserManager.AddToRoleAsync(user, userDto.Role);
                
                await Database.SaveAsync();
                
                return new OperationDetails(true, "Registration successfully completed!", "");
            }
            else
            {
                return new OperationDetails(false, "User with this login already exists", "Email");
            }
        }

        public async Task<bool> Authenticate(UserDTO userDto)
        {

            var user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                return false;
            }
            //var username = user.UserName;
            var email = user.Email;

            var auth = await Database.SignInManager.PasswordSignInAsync(email, userDto.Password, false, lockoutOnFailure: false);

            return auth.Succeeded;
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
