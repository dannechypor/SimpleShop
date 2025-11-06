namespace Model.Entities;

public sealed class WhishListItem
{
    public int ProductId { get; set; }
    public Product Product { get; set; }


    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}