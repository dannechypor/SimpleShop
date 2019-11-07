using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
   public class MyWishListViewModel
    {
        public List<Product> Products { get; set;}

        public MyWishListViewModel()
        {
            Products = new List<Product>();
        }
    }
}
