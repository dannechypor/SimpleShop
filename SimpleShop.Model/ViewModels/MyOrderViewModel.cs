using System.Collections.Generic;
using SimpleShop.Model.DTO;

namespace SimpleShop.Model.ViewModels;

public class MyOrderViewModel
{
    public IEnumerable<MyOrderInfoDto> Orders { get; set; }
    public OrderDto Statust { get; set; }
}