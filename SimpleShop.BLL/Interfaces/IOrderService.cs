using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities.Enums;

namespace SimpleShop.BLL.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(OrderDto orderDto);
    List<MyOrderInfoDto> GetAllOrdersAsync(string userId);
    List<MyOrderInfoDto> GetAllOrdersAsync();
    Task<OperationDetails> ChangeOrderStatus(int oederId, OrderStatus status);
}