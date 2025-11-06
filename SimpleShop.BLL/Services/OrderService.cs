using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SimpleShop.BLL.Infrastructure;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.DAL.Interfaces;
using SimpleShop.Model.DTO;
using SimpleShop.Model.Entities;
using SimpleShop.Model.Entities.Enums;

namespace SimpleShop.BLL.Services;

public class OrderService(
    IMapper mapper,
    IShoppingCartService shoppingCartService,
    IUnitOfWork db,
    ApplicationDbContext context,
    ILogger<OrderService> logger)
    : IOrderService
{
    public async Task CreateOrderAsync([FromForm] OrderDto orderDto)
    {
        if (orderDto == null)
        {
            logger.LogError("CreateOrderAsync: orderDto is null");
            throw new ArgumentNullException(nameof(orderDto));
        }

        var shoppingCartItems = await shoppingCartService.GetShoppingCartItemsAsync();
        if (shoppingCartItems == null || !shoppingCartItems.Any())
        {
            logger.LogWarning("CreateOrderAsync: Attempted to create order with empty shopping cart");
            throw new InvalidOperationException("Cannot create order with empty shopping cart");
        }

        var cartTotal = (await shoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;

        // Check if database provider supports transactions (In-Memory Database doesn't)
        var isInMemory = context.Database.ProviderName?.Contains("InMemory") == true;
        var canUseTransactions = !isInMemory;
        
        // Use database transaction to ensure atomicity (if supported)
        Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction? transaction = null;
        if (canUseTransactions)
        {
            transaction = await context.Database.BeginTransactionAsync();
        }
        
        try
        {
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

            // Clear cart only after successful order creation
            await shoppingCartService.ClearCartAsync();

            if (transaction != null)
            {
                await transaction.CommitAsync();
            }
            
            logger.LogInformation("Order created successfully. OrderId: {OrderId}, Total: {Total}",
                order.OrderId, cartTotal);
        }
        catch (Exception ex)
        {
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            logger.LogError(ex, "Error creating order. UserId: {UserId}", orderDto.UserId);
            throw;
        }
        finally
        {
            if (transaction != null)
            {
                await transaction.DisposeAsync();
            }
        }
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
        if (oederId == 0)
        {
            logger.LogWarning("ChangeOrderStatus: Order ID is 0");
            return new OperationDetails(false, "Order ID cannot be 0", "");
        }

        var order = db.OrderRepository.GetById(oederId);
        if (order == null)
        {
            logger.LogWarning("ChangeOrderStatus: Order not found. OrderId: {OrderId}", oederId);
            return new OperationDetails(false, "Order not found", "");
        }

        try
        {
            order.Status = status;
            db.OrderRepository.Update(order);
            await db.SaveAsync();
            logger.LogInformation("Order status updated. OrderId: {OrderId}, NewStatus: {Status}",
                oederId, status);
            return new OperationDetails(true, "Order status updated successfully", "");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating order status. OrderId: {OrderId}", oederId);
            return new OperationDetails(false, "Error updating order status", "");
        }
    }
}