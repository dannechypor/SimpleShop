using System.Collections.Generic;
using SimpleShop.Model.Entities;

namespace SimpleShop.Model.DTO;

public class MyWishListDto
{
    public List<Product> Products { get; set; } = [];
}