using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleShop.Model.Entities;

namespace SimpleShop.Model.ViewModels;

public class FilterViewModel
{
    public FilterViewModel(List<Product> products, int? product, string name)
    {
        Products = new SelectList(products, "Id", "Name", product);
        SelectedProduct = product;
        SelectedName = name;
    }

    public SelectList Products { get; private set; }
    public int? SelectedProduct { get; private set; }
    public string SelectedName { get; private set; }
    public string Size { get; set; }
}