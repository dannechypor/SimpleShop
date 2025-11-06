using Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class TopSellingViewModel
    {
      public IEnumerable<TopSellingDTO> Tops { get; set; }
    }
}
