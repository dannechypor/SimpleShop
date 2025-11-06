namespace SimpleShop.Model.Entities;

public sealed class OrderDetail : BaseEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int Amount { get; set; }
    public decimal Price { get; set; }


    public Product Product { get; set; }
    public int ProductId { get; set; }

    public Order Order { get; set; }
    public int OrderId { get; set; }
}