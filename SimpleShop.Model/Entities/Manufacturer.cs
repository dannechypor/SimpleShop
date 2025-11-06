using System.Collections.Generic;

namespace SimpleShop.Model.Entities;

public sealed class Manufacturer : BaseEntity
{
    public int ManufacturerId { get; set; }
    public string Name { get; set; }

    public IEnumerable<Product> Products { get; set; }
}