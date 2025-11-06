namespace SimpleShop.Model.DTO;

public class ProductOrderInfoDto
{
    public int Amount { get; set; }
    public decimal Price { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
}