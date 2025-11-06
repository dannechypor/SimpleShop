using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class RegisterViewModel
{
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [RegularExpression(@"^\+[0-9]{11,12}$", ErrorMessage = "Wrong phone number")]
    public string PhoneNumber { get; set; }

    [Required] public string Address { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 2)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}