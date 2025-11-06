using System.Collections.Generic;
using Model.DTO;
using Model.Entities;
using Model.Models;

namespace Model.ViewModels;

public class CatalogProductListViewModel
{
    public IEnumerable<Product> Products { get; set; }
    public ProductDto Product { get; set; }
    public FilterModel FilterModel { get; set; }
    public IndexViewModel<Product> IndexViewModel { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Manufacturer> Manufacturers { get; set; }
    public ReviewViewModel Review { get; set; }
}