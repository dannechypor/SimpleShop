using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public CategoryDTO()
        {
            Categories = new List<Category>();
        }

    }
}
