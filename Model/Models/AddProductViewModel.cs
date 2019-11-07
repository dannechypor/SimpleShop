using Model.Entities;
using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class AddProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public ProductViewModel Product { get; set; }

      

        public IEnumerable<Manufacturer> Manufacturers { get; set; }

        public AddProductViewModel()
        {
            Categories = new List<Category>();
            Manufacturers = new List<Manufacturer>();
        }
    }
}
