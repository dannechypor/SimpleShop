using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Model.DTO;
using SimpleShop.Model.ViewModels;

namespace SimpleShop.Web.Controllers;

public class OrderController(
    IShoppingCartService shoppingCartService,
    IOrderService orderService,
    IMapper mapper)
    : Controller
{
    public async Task<IActionResult> Checkout()
    {
        // Check if user is authenticated
        if (!User.Identity!.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "Please login to continue with checkout";
            return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Order") });
        }

        var cartItems = await shoppingCartService.GetShoppingCartItemsAsync();
        if (cartItems?.Count() <= 0) return RedirectToAction("Index", "ShoppingCart");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(OrderViewModel model)
    {
        // Check if user is authenticated
        if (!User.Identity!.IsAuthenticated)
        {
            TempData["ErrorMessage"] = "Please login to continue with checkout";
            return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Order") });
        }

        if (!ModelState.IsValid) return View(model);

        var cartItems = await shoppingCartService.GetShoppingCartItemsAsync();

        if (cartItems?.Count() <= 0)
        {
            ModelState.AddModelError("", "Your Cart is empty. Please add some products before checkout");
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