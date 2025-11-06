using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entities;
using Model.ViewModels;
using System;
using System.Threading.Tasks;

namespace SimpleShop.Web.Controllers;

public class ProfileController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ILogger<ProfileController> logger)
    : Controller
{
    private readonly UserManager<ApplicationUser> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    private readonly ILogger<ProfileController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [TempData]
    public string StatusMessage { get; set; }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var model = new PersonViewModel
        {
            Id = user.Id,
            Email = user.Email,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            StatusMessage = StatusMessage
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(PersonViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        if (model.Email != user.Email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
            if (!setEmailResult.Succeeded)
                throw new ApplicationException(
                    $"Unexpected error occurred setting email for user with ID '{user.Id}'.");
        }

        if (model.PhoneNumber != user.PhoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
            if (!setPhoneResult.Succeeded)
                throw new ApplicationException(
                    $"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
        }

        var requiresUpdate = false;

        if (model.Address != user.Address)
        {
            user.Address = model.Address;
            requiresUpdate = true;
        }

        if (model.FirstName != user.FirstName)
        {
            user.FirstName = model.FirstName;
            requiresUpdate = true;
        }

        if (model.LastName != user.LastName)
        {
            user.LastName = model.LastName;
            requiresUpdate = true;
        }

        if (requiresUpdate)
        {
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                throw new ApplicationException(
                    $"Unexpected error occurred updating user with ID '{user.Id}'.");
        }

        StatusMessage = "Your profile has been updated";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
        {
            return RedirectToAction(nameof(SetPassword));
        }

        var model = new ChangeUserPasswordModel { StatusMessage = StatusMessage };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangeUserPasswordModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var changePasswordResult =
            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            AddErrors(changePasswordResult);
            return View(model);
        }

        await _signInManager.SignInAsync(user, false);
        _logger.LogInformation("User changed their password successfully.");
        StatusMessage = "Your password has been changed.";

        return RedirectToAction(nameof(ChangePassword));
    }

    [HttpGet]
    public async Task<IActionResult> SetPassword()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (hasPassword)
        {
            return RedirectToAction(nameof(ChangePassword));
        }

        var model = new SetPasswordViewModel { StatusMessage = StatusMessage };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            AddErrors(addPasswordResult);
            return View(model);
        }

        await _signInManager.SignInAsync(user, isPersistent: false);
        StatusMessage = "Your password has been set.";

        return RedirectToAction(nameof(SetPassword));
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
    }
}