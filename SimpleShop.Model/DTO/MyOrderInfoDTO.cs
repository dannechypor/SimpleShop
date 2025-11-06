using System.Collections.Generic;

namespace Model.DTO;

public class MyOrderInfoDto
{
    public OrderDto Orders { get; set; }
    public IEnumerable<ProductOrderInfoDto> ProductOrderInfos { get; set; }
}