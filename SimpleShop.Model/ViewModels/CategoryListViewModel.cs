using System.Collections.Generic;
using Model.Entities;

namespace Model.ViewModels;

public class CategoryListViewModel
{

    public IEnumerable<Category> Categories { get; set; }

    public CategoryListViewModel()
    {
        Categories = new List<Category>();
    }
}