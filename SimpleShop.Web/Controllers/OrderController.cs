using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.ViewModels;
using Shop.BLL.Interfaces;
using SimpleShop.BLL.Interfaces;

namespace ForGoodTime.Controllers;

public class OrderController(
    IShoppingCartService shoppingCartService,
    IOrderService orderService,
    IMapper mapper)
    : Controller
{
    public async Task<IActionResult> Checkout()
    {
        var cartItems = await shoppingCartService.GetShoppingCartItemsAsync();
        if (cartItems?.Count() <= 0) return RedirectToAction("Index", "ShoppingCart");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(OrderViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var cartItems = await shoppingCartService.GetShoppingCartItemsAsync();

        if (cartItems?.Count() <= 0)
        {
            ModelState.AddModelError("", "Your Cart is empty. Please add some cakes before checkout");
            return View(model);
        }

        var order = mapper.Map<OrderViewModel, OrderDto>(model);
        order.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await orderService.CreateOrderAsync(order);

        await shoppingCartService.ClearCartAsync();


        return View("Confirmation");
    }


    public IActionResult OrderList()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (User.Identity!.IsAuthenticated && User.IsInRole("Admin"))
        {
            var orders = orderService.GetAllOrdersAsync();

            return View(new MyOrderViewModel
            {
                Orders = orders
            });
        }
        else
        {
            var orders = orderService.GetAllOrdersAsync(userId);

            return View(new MyOrderViewModel
            {
                Orders = orders
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> OrderList(MyOrderViewModel model)
    {
        await orderService.ChangeOrderStatus(model.Statust.OrderId, model.Statust.Status);

        return RedirectToAction("OrderList");
    }
}