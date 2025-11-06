using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public ProductViewModel Product { get; set; }
        public CategoryViewModel Category { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public HomeViewModel()
        {
            Categories = new List<Category>();
            Products = new List<Product>();
        }
    }
}
