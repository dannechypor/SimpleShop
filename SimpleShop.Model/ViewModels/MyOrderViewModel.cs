using System.Collections.Generic;
using Model.DTO;

namespace Model.ViewModels;

public class MyOrderViewModel
{
    public IEnumerable<MyOrderInfoDto> Orders { get; set; }
    public OrderDto Statust { get; set; }
}