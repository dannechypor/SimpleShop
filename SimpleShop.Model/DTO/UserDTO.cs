using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
                  
    }
}
