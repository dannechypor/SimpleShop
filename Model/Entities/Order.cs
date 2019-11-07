using Model.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class Order
    {   
        public int OrderId { get; set; }       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber{ get; set; }
        public DateTime OrderPlaced { get; set; }
        public OrderStatus Status { get; set; }
        public string Message { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public IEnumerable<OrderDetail> OrderLines { get; set; }
    }
}
