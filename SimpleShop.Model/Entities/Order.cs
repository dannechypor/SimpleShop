using System;
using System.Collections.Generic;
using Model.Entities;
using Model.Entities.Enums;

namespace SimpleShop.Model.Entities;

public sealed class Order : BaseEntity
{
    public int OrderId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime OrderPlaced { get; set; }
    public OrderStatus Status { get; set; }
    public string Message { get; set; }
    public decimal OrderTotal { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public IEnumerable<OrderDetail> OrderLines { get; set; }
}