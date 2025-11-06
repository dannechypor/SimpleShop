using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Models;

namespace ForGoodTime.Controllers;

public class ProfileController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ILogger<ProfileController> logger,
    string statusMessage)
    : Controller
{
    private readonly ILogger _logger = logger;


    [TempData] private string StatusMessage { get; set; } = statusMessage;


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(PersonViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await userManager.GetUserAsync(User);


            var email = user.Email;
            if (model.Email != email)
            {
                var setUserNAmeResult = await userManager.SetEmailAsync(user, model.Email);
                if (!setUserNAmeResult.Succeeded)
                    throw new ApplicationException(
                        $"Unexpected error occurred setting Email for user with ID '{user.Id}'.");
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                    throw new ApplicationException(
                        $"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            }

            var address = user.Address;
            if (model.Address != address)
            {
                user.Address = model.Address;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new ApplicationException(
                        $"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            }

            var firstname = user.FirstName;
            if (model.FirstName != firstname)
            {
                user.FirstName = model.FirstName;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new ApplicationException(
                        $"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            }

            var lastname = user.LastName;
            if (model.LastName != lastname)
            {
                user.LastName = model.LastName;
                var result = await userManager.UpdateAsync(user);
                if (!result.Succeeded)
                    throw new ApplicationException(
                        $"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            }
        }


        StatusMessage = "Your profile has been updated";
        return RedirectToAction("Index");
    }


    [HttpGet]
    public async Task<IActionResult> ChangePassword()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");

        var hasPassword = await userManager.HasPasswordAsync(user);
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

        var user = await userManager.GetUserAsync(User);
        if (user == null)
            throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");

        var changePasswordResult =
            await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        if (!changePasswordResult.Succeeded)
        {
            AddErrors(changePasswordResult);
            return View(model);
        }

        await signInManager.SignInAsync(user, false);
        _logger.LogInformation("User changed their password successfully.");
        StatusMessage = "Your password has been changed.";

        return RedirectToAction(nameof(ChangePassword));
    }

    [HttpGet]
    public async Task<IActionResult> SetPassword()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }
    
        var hasPassword = await userManager.HasPasswordAsync(user);
    
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
    
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            throw new ApplicationException($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
        }
    
        var addPasswordResult = await userManager.AddPasswordAsync(user, model.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            AddErrors(addPasswordResult);
            return View(model);
        }
    
        await signInManager.SignInAsync(user, isPersistent: false);
        StatusMessage = "Your password has been set.";
    
        return RedirectToAction(nameof(SetPassword));
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await userManager.GetUserAsync(User);
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
}