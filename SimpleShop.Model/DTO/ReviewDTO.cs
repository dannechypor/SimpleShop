using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
   public class ReviewDTO
   {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
   }
}
