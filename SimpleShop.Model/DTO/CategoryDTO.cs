using Model.Entities;
using System.Collections.Generic;

namespace Model.DTO;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; }

    public IEnumerable<Category> Categories { get; set; }
}