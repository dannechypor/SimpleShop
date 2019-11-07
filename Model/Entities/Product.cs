using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Hight { get; set; }
        public string Width { get; set; }
        public string Deepth { get; set; }
        public string ImageUrl_1 { get; set; }
        public string ImageUrl_2 { get; set; }
        public string ImageUrl_3 { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public bool? InStock { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual IEnumerable<OrderDetail> Orders { get; set; }

        public int? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public IEnumerable<Review> Comments { get; set; }

      
    }
}
