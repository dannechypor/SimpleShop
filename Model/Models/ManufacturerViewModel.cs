using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class ManufacturerViewModel
    {
        public int ManufacturerId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
