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

namespace Shop.BLL.Services;

public class OrderService(
    IMapper mapper,
    IShoppingCartService shoppingCartService,
    IUnitOfWork db)
    : IOrderService
{
    public async Task CreateOrderAsync([FromForm] OrderDto orderDto)
    {
        var order = mapper.Map<OrderDto, Order>(orderDto);
        order.OrderPlaced = DateTime.UtcNow;
        order.Status = OrderStatus.Processing;
        db.OrderRepository.Add(order);


        var shoppingCartItems = await shoppingCartService.GetShoppingCartItemsAsync();
        orderDto.OrderTotal = (await shoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;
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
                    OrderPlaced = e.OrderPlaced
                    //Status = e.Status
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
        var order = db.OrderRepository.GetById(oederId);
        order.Status = status;

        db.OrderRepository.Update(order);
        await db.SaveAsync();
        return new OperationDetails(true, "", "");
    }
}