using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.Entities
{

    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<Review> Comments { get; set; }

      

      
    }
}
