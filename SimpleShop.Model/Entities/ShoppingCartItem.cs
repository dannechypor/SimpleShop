namespace SimpleShop.Model.Entities;

public sealed class ShoppingCartItem
{
    public int ShoppingCartItemId { get; set; }
    public int Amount { get; set; }
    public string ShoppingCartId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}