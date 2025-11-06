using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models;
using Model.ViewModels;
using Shop.DAL.Interfaces;

namespace ForGoodTime.Controllers;

[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private IUnitOfWork Database { get; }

    public UsersController(UserManager<ApplicationUser> userManager, IUnitOfWork uow)
    {
        _userManager = userManager;
        Database = uow;
    }

    public IActionResult Index()
    {
        return View(_userManager.Users.ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        var user = await Database.UserManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            if (ModelState.IsValid)
            {
                user = new ApplicationUser
                {
                    Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber, Address = model.Address
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        ModelState.AddModelError("", "User with this login already exists!");
        return View(model);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        var model = new EditUserViewModel
        {
            Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName,
            PhoneNumber = user.PhoneNumber, Address = user.Address
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Address = model.Address;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded) return RedirectToAction("Index");

                foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
        }

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> ChangePassword(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return NotFound();
        var model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var user = await _userManager.FindByIdAsync(model.Id);
        if (user != null)
        {
            var _passwordValidator =
                HttpContext.RequestServices.GetService(typeof(IPasswordValidator<ApplicationUser>)) as
                    IPasswordValidator<ApplicationUser>;
            var _passwordHasher =
                HttpContext.RequestServices.GetService(typeof(IPasswordHasher<ApplicationUser>)) as
                    IPasswordHasher<ApplicationUser>;

            var result =
                await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
            if (result.Succeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
        }
        else
        {
            ModelState.AddModelError(string.Empty, "User not found");
        }

        return View(model);
    }
}