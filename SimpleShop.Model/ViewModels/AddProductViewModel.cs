using System.Collections.Generic;
using Model.Entities;
using Model.Models;

namespace Model.ViewModels;

public class AddProductViewModel
{
    public IEnumerable<Category> Categories { get; set; }
    public ProductViewModel Product { get; set; }

    public IEnumerable<Manufacturer> Manufacturers { get; set; }
}