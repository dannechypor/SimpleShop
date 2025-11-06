using Model.DTO;
using Model.Entities;
using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
   public class CatalogProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public ProductDTO Product { get; set; }
        public FilterModel FilterModel { get; set; }
        public IndexViewModel<Product> IndexViewModel { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public ReviewViewModel Review { get; set; }
      
    }
}
