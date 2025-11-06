using System.Collections.Generic;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities.Enums;
using Shop.BLL.Infrastructure;

namespace Shop.BLL.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(OrderDto orderDto);
    List<MyOrderInfoDto> GetAllOrdersAsync(string userId);
    List<MyOrderInfoDto> GetAllOrdersAsync();
    Task<OperationDetails> ChangeOrderStatus(int oederId, OrderStatus status);
}