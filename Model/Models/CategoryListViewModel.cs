using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class CategoryListViewModel
    {

        public IEnumerable<Category> Categories { get; set; }

        public CategoryListViewModel()
        {
            Categories = new List<Category>();
        }
    }
}
