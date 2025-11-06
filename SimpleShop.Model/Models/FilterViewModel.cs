using Microsoft.AspNetCore.Mvc.Rendering;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Product> products, int? product, string name)
    {
        
        Products = new SelectList(products, "Id", "Name", product);
        SelectedProduct = product;
        SelectedName = name;

    }
    public SelectList Products { get; private set; } // список продуктів
    public int? SelectedProduct { get; private set; }   // вибраний продукт
    public string SelectedName { get; private set; }    // введене імя
    public string Size { get;  set; }    // введене імя
 
}
}
