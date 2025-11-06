using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class OrderViewModel
    {

        public int OrderId { get; set; }
 
        public DateTime OrderPlaced { get; set; }
        public decimal OrderTotal { get; set; }

        public string UserId { get; set; }

        [StringLength(255)]
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [StringLength(255)]
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [StringLength(255)]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address")]
        public string Address { get; set; }
       
        [StringLength(10)]
        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

