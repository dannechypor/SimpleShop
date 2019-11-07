using Model.DTO;
using Model.Entities;
using Model.Entities.Enums;
using Shop.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDTO orderDto);
        List<MyOrderInfoDTO> GetAllOrdersAsync(string userId);
        List<MyOrderInfoDTO> GetAllOrdersAsync();
        Task<OperationDetails> ChangeOrderStatus(int oederId, OrderStatus status);
    }
}
