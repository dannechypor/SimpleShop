using System.Collections.Generic;
using SimpleShop.Model.Entities;

namespace SimpleShop.Model.ViewModels;

public class AddProductViewModel
{
    public IEnumerable<Category> Categories { get; set; }
    public ProductViewModel Product { get; set; }

    public IEnumerable<Manufacturer> Manufacturers { get; set; }
}