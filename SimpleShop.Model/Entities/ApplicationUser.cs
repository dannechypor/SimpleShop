using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SimpleShop.Model.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string Address { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public IEnumerable<Order> Orders { get; set; }

    public IEnumerable<Review> Comments { get; set; }
}