using System;

namespace SimpleShop.Model.Entities;

public sealed class Review : BaseEntity
{
    public string Text { get; set; }
    public int Rating { get; set; }
    public DateTime Date { get; set; }


    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}