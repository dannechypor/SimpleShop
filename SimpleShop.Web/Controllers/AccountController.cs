using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Model.ViewModels;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;

namespace ForGoodTime.Controllers;

public class AccountController(SignInManager<ApplicationUser> authenticationManager, IUserService userService)
    : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var userDto = new UserDto { Email = model.Email, Password = model.Password };
        var auth = await userService.Authenticate(userDto);
        if (!auth)
            ModelState.AddModelError("", "Incorrect Login or Password");
        else
            return RedirectToAction("Index", "Home");

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
        if (!ModelState.IsValid) return View(model);

        var userDto = new UserDto
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber,
            Role = "user",
            Password = model.Password
        };

        var operationDetails = await userService.Create(userDto);

        if (operationDetails.Succedeed) return RedirectToAction("Index", "Home");

        ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await authenticationManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}