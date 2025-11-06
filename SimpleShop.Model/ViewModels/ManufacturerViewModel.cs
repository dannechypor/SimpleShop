using System.ComponentModel.DataAnnotations;

namespace SimpleShop.Model.ViewModels;

public class ManufacturerViewModel
{
    public int ManufacturerId { get; set; }
    [Required] public string Name { get; set; }
}