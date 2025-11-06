using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class ProductOrderInfoDTO
    {
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
