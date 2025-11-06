using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class CreateUserViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] public string Address { get; set; }


    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 2)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
}