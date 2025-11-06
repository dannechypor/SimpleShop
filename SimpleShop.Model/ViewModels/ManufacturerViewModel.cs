using System.ComponentModel.DataAnnotations;

namespace Model.ViewModels;

public class ManufacturerViewModel
{
    public int ManufacturerId { get; set; }
    [Required] public string Name { get; set; }
}