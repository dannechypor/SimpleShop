using System.Collections.Generic;
using Model.Entities;

namespace Model.DTO;

public class MyWishListDto
{
    public List<Product> Products { get; set; } = [];
}