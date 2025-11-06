using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }   
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

    }
}
