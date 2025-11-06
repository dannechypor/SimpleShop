using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class LoginViewModel
{
    [Required] public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember?")] public bool RememberMe { get; set; }
}