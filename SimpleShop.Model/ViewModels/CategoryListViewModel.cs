using System.Collections.Generic;
using SimpleShop.Model.Entities;

namespace SimpleShop.Model.ViewModels;

public class CategoryListViewModel
{
    public CategoryListViewModel()
    {
        Categories = new List<Category>();
    }

    public IEnumerable<Category> Categories { get; set; }
}