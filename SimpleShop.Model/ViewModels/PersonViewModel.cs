using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class PersonViewModel
{
    public string Id { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string FirstName { get; set; }
    [Required] public string LastName { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] public string Address { get; set; }

    public string StatusMessage { get; set; }
}