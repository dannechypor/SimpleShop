using System.Collections.Generic;
using Model.Entities;

namespace SimpleShop.Model.Entities;

public sealed class Category : BaseEntity
{
    public string Name { get; set; }

    public IEnumerable<Product> Products { get; set; }
}