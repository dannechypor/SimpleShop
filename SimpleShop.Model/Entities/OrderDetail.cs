using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class OrderDetail 
    {
        public int OrderDetailId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
       

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
