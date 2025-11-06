using Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Models
{
    public class CategoryViewModel
    {

        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
      
    }
}
