using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Model.Entities.Enums;
using Model.Models;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.DAL;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Services
{
    public class OrderService : IOrderService
    {      
        
        private readonly IUnitOfWork Database;       
        private readonly IShoppingCartService ShoppingCartService;
        private readonly IMapper _mapper;
        public OrderService(IMapper mapper,
             IShoppingCartService shoppingCartService,
            IUnitOfWork db )
          
        {

            ShoppingCartService = shoppingCartService;
            _mapper = mapper;
            Database = db;
          
        }

        public async Task CreateOrderAsync([FromForm]OrderDTO orderDto)
        {
            var order = _mapper.Map<OrderDTO, Order>(orderDto);
            order.OrderPlaced = DateTime.UtcNow;
            order.Status = OrderStatus.Processing;
            Database.OrderRepository.Add(order);
           

            var shoppingCartItems = await ShoppingCartService.GetShoppingCartItemsAsync();
            orderDto.OrderTotal = (await ShoppingCartService.GetCartCountAndTotalAmmountAsync()).TotalAmmount;
            var orderDetail = (shoppingCartItems.Select(e => new OrderDetail
            {
                Amount = e.Amount,
                ProductId = e.Product.Id,
                Name = e.Product.Name,
                Image = e.Product.ImageUrl_1,
                OrderId = order.OrderId,
                Price = e.Product.Price,                

            }));

            Database.OrderDetailRepository.AddRange(orderDetail);
            await Database.SaveAsync();
            
        
        }

      
        public List<MyOrderInfoDTO> GetAllOrdersAsync(string userId)
        {
            var orders = Database.OrderRepository.All().Where(e => e.UserId == userId)
            .Include(e => e.OrderLines)
                .Select(e => new MyOrderInfoDTO
                {
                    Orders = new OrderDTO
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
                    ProductOrderInfos = e.OrderLines.Select(o => new ProductOrderInfoDTO
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

        public List<MyOrderInfoDTO> GetAllOrdersAsync()
        {
            var orders = Database.OrderRepository.All()
            .Include(e => e.OrderLines)
                .Select(e => new MyOrderInfoDTO
                {
                    Orders = new OrderDTO
                    {
                        OrderId = e.OrderId,
                        Address = e.Address,
                        Email = e.Email,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        PhoneNumber = e.PhoneNumber,
                        OrderPlaced = e.OrderPlaced,
                        //Status = e.Status
                    },
                    ProductOrderInfos = e.OrderLines.Select(o => new ProductOrderInfoDTO
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
            var order = Database.OrderRepository.GetById(oederId);
            order.Status = status;

            Database.OrderRepository.Update(order);
            await Database.SaveAsync(); 
            return new OperationDetails(true, "", "");
        }
    }

}

