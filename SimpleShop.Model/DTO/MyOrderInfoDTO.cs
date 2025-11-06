using System.Collections.Generic;

namespace SimpleShop.Model.DTO;

public class MyOrderInfoDto
{
    public OrderDto Orders { get; set; }
    public IEnumerable<ProductOrderInfoDto> ProductOrderInfos { get; set; }
}