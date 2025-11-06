using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
   public class OrderDTO
    {
        public int OrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime OrderPlaced { get; set; }
        public OrderStatus Status { get; set; }
        public decimal OrderTotal { get; set; }

        public string UserId { get; set; }
    }
}
