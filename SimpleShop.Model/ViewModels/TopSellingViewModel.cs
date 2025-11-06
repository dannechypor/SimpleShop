using System.Collections.Generic;
using Model.DTO;

namespace Model.ViewModels;

public class TopSellingViewModel
{
    public IEnumerable<TopSellingDto> Tops { get; set; }
}