using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Manufacturer 
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
