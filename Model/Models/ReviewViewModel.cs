using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [HiddenInput]
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
