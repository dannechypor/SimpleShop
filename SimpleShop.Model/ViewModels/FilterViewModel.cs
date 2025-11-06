using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Entities;

namespace Model.ViewModels;

public class FilterViewModel
{
    public SelectList Products { get; private set; }
    public int? SelectedProduct { get; private set; }
    public string SelectedName { get; private set; } 
    public string Size { get; set; } 
    
    public FilterViewModel(List<Product> products, int? product, string name)
    {
        Products = new SelectList(products, "Id", "Name", product);
        SelectedProduct = product;
        SelectedName = name;
    }
}