using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Model.Entities.Enums;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL.Interfaces;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Model.Entities;

namespace Shop.BLL.Services;

public class OrderService(
    IMapper mapper,
    IShoppingCartService shoppingCartService,
    IUnitOfWork db)
    : IOrderService
{
    public async Task CreateOrderAsync([FromForm] OrderDto orderDto)
    {
        if (orderDto == null)
            throw new ArgumentNullException(nameof(orderDto));

        var shoppingCartItems = await shoppingCartService.GetShoppingCartItemsAsync();
        if (shoppingCartItems == null || !shoppingCartItems.Any())
            throw new InvalidOperationException("Cannot create order with empty shopping cart");

        var cartTotal = (await shoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;
        
        var order = mapper.Map<OrderDto, Order>(orderDto);
        order.OrderPlaced = DateTime.UtcNow;
        order.Status = OrderStatus.Processing;
        order.OrderTotal = cartTotal;
        db.OrderRepository.Add(order);

        var orderDetail = shoppingCartItems.Select(e => new OrderDetail
        {
            Amount = e.Amount,
            ProductId = e.Product.Id,
            Name = e.Product.Name,
            Image = e.Product.ImageUrl_1,
            OrderId = order.OrderId,
            Price = e.Product.Price
        });

        db.OrderDetailRepository.AddRange(orderDetail);
        await db.SaveAsync();
    }


    public List<MyOrderInfoDto> GetAllOrdersAsync(string userId)
    {
        var orders = db.OrderRepository.All().Where(e => e.UserId == userId)
            .Include(e => e.OrderLines)
            .Select(e => new MyOrderInfoDto
            {
                Orders = new OrderDto
                {
                    OrderId = e.OrderId,
                    Address = e.Address,
                    Email = e.Email,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    OrderPlaced = e.OrderPlaced,
                    Status = e.Status
                },
                ProductOrderInfos = e.OrderLines.Select(o => new ProductOrderInfoDto
                {
                    ProductId = o.ProductId,
                    Name = o.Name,
                    Image = o.Image,
                    Price = o.Price,
                    Amount = o.Amount
                })
            }).ToList();

        return orders;
    }

    public List<MyOrderInfoDto> GetAllOrdersAsync()
    {
        var orders = db.OrderRepository.All()
            .Include(e => e.OrderLines)
            .Select(e => new MyOrderInfoDto
            {
                Orders = new OrderDto
                {
                    OrderId = e.OrderId,
                    Address = e.Address,
                    Email = e.Email,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    PhoneNumber = e.PhoneNumber,
                    OrderPlaced = e.OrderPlaced,
                    Status = e.Status
                },
                ProductOrderInfos = e.OrderLines.Select(o => new ProductOrderInfoDto
                {
                    ProductId = o.ProductId,
                    Name = o.Name,
                    Image = o.Image,
                    Price = o.Price,
                    Amount = o.Amount
                })
            }).ToList();

        return orders;
    }

    public async Task<OperationDetails> ChangeOrderStatus(int oederId, OrderStatus status)
    {
        if (oederId == 0) return new OperationDetails(false, "Order ID cannot be 0", "");
        
        var order = db.OrderRepository.GetById(oederId);
        if (order == null) return new OperationDetails(false, "Order not found", "");

        order.Status = status;
        db.OrderRepository.Update(order);
        await db.SaveAsync();
        return new OperationDetails(true, "Order status updated successfully", "");
    }
}