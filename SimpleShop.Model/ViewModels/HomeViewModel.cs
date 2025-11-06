using System.Collections.Generic;
using SimpleShop.Model.Entities;

namespace Model.ViewModels;

public class HomeViewModel
{
    public IEnumerable<Category> Categories { get; set; }
    public ProductViewModel Product { get; set; }
    public CategoryViewModel Category { get; set; }
    public IEnumerable<Product> Products { get; set; }
}