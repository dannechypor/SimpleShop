using System.Collections.Generic;
using SimpleShop.Model.DTO;

namespace SimpleShop.Model.ViewModels;

public class TopSellingViewModel
{
    public IEnumerable<TopSellingDto> Tops { get; set; }
}