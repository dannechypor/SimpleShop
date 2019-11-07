using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    public class MyWishListDTO
    {
        public List<Product> Products { get; set; }

        public MyWishListDTO()
        {
            Products = new List<Product>();
        }
    }
}
