using System.Collections.Generic;
using Model.DTO;
using SimpleShop.Model.Entities;

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
    public Dictionary<int, int> CategoryCounts { get; set; }
    public Dictionary<int, int> ManufacturerCounts { get; set; }
    public int? CurrentCategoryId { get; set; }
    public int? CurrentManufacturerId { get; set; }
    public string CurrentSearch { get; set; }
    public decimal? CurrentMinPrice { get; set; }
    public decimal? CurrentMaxPrice { get; set; }
    public SortState CurrentSortOrder { get; set; }
    public int AllProductsCount { get; set; }
    public decimal MinPriceValue { get; set; }
    public decimal MaxPriceValue { get; set; }
}