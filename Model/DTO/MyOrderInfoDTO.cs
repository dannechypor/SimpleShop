using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
  public  class MyOrderInfoDTO
    {
        public OrderDTO Orders { get; set; }
        public IEnumerable<ProductOrderInfoDTO> ProductOrderInfos { get; set; }
    }
}
