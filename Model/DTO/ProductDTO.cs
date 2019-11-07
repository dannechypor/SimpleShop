using Model.Entities;
using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Hight { get; set; }
      
        public string Deepth { get; set; }
        public string Width { get; set; }
        public string ShortDescription { get; set; }        
        public string LongDescription { get; set; }
    
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public bool? InStock { get; set; }
        public string ImageUrl_1 { get; set; }
        public string ImageUrl_2 { get; set; }
        public string ImageUrl_3 { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public Category Category { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Product> Products { get; set; }
       
        public ProductDTO()
        {
            Categories = new List<Category>();
            Products = new List<Product>();
            Manufacturers = new List<Manufacturer>();
        }

    }
}
