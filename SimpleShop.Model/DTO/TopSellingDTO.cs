using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class TopSellingDTO
    {
        public string Name { get; set; }
        public string Image { get; set; }     
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
