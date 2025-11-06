using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
   public class Review
    {
        public int Id { get; set; }     
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
