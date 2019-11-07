using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;



namespace FriendsAndTravel.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> AuthenticationManager;
       

        public AccountController(SignInManager<ApplicationUser> authenticationManager,IUserService userService )
        {

            AuthenticationManager = authenticationManager;
            _userService = userService;

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                bool auth = await _userService.Authenticate(userDto);
                if (!auth)
                {
                    ModelState.AddModelError("", "Incorrect Login or Password");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
           
            if (ModelState.IsValid)
            {

                UserDTO userDto = new UserDTO
                {

                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Role = "user" ,
                    Password = model.Password 
                    
                };
                OperationDetails operationDetails = await _userService.Create(userDto);

                if (operationDetails.Succedeed)
                {

              
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);

                }
            }
            return View(model);
        }


        
        public async Task<IActionResult> Logout()
        {
            await AuthenticationManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Index()
        {
            return View();
        }

    }
}
         //Update