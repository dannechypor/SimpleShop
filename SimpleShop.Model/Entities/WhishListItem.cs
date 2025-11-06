using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.Entities
{
   public class WhishListItem
    {
       

       
        public int ProductId { get; set; }
        public virtual Product Product { get; set;}

       
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
