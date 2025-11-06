using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class CategoryViewModel
{
    public int CategoryId { get; set; }

    [Required] public string Name { get; set; }
}