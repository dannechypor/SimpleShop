using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForGoodTime.Controllers
{
    public class OrderController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;


        public OrderController(
                IShoppingCartService shoppingCartService,
                IOrderService orderService,
                IMapper mapper
               )
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _mapper = mapper;

        }

        public async Task<IActionResult> Checkout()
        {
            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();
            if (cartItems?.Count() <= 0)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var cartItems = await _shoppingCartService.GetShoppingCartItemsAsync();

            if (cartItems?.Count() <= 0)
            {
                ModelState.AddModelError("", "Your Cart is empty. Please add some cakes before checkout");
                return View(model);
            }

            var order = _mapper.Map<OrderViewModel, OrderDTO>(model);
            order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _orderService.CreateOrderAsync(order);

            await _shoppingCartService.ClearCartAsync();


            return View("Confirmation");
        }


        public IActionResult OrderList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                var orders = _orderService.GetAllOrdersAsync();

                return View(new MyOrderViewModel
                {
                    Orders = orders
                });
            }
            else
            {
                var orders = _orderService.GetAllOrdersAsync(userId);

                return View(new MyOrderViewModel
                {
                    Orders = orders
                });

            }

        }

        [HttpPost]
        public async Task<IActionResult> OrderList(MyOrderViewModel model)
        {
            await _orderService.ChangeOrderStatus(model.Statust.OrderId, model.Statust.Status);

            return RedirectToAction("OrderList");
        }
    }
}

