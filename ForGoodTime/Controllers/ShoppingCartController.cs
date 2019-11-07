using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using ForGoodTime.Components;
using ForGoodTime.Views.Shared.Components.ShoppingCartSummary;

namespace ForGoodTime.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartController(IProductRepository productRepository, IShoppingCartService shoppingCart)
        {
            _productRepository = productRepository;
            _shoppingCart = shoppingCart;
        }

        public async Task<IActionResult> Index()
        {
            await _shoppingCart.GetShoppingCartItemsAsync();
            var shoppingCartCountTotal = await _shoppingCart.GetCartCountAndTotalAmmountAsync();
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartItemsTotal = shoppingCartCountTotal.ItemCount,
                ShoppingCartTotal = shoppingCartCountTotal.TotalAmmount,
            };


            return View(shoppingCartViewModel);
        }
      
        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(int id)
        {
            var selectedCake = _productRepository.GetById(id);
            if (selectedCake == null)
            {
                return NotFound();
            }

            await _shoppingCart.AddToCartAsync(selectedCake);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddToShoppingCartGet(int id)
        {
            var selectedCake = _productRepository.GetById(id);
            if (selectedCake == null)
            {
                return NotFound();
            }

            await _shoppingCart.AddToCartAsync(selectedCake);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromShoppingCart(int id)
        {
            var selectedCake =  _productRepository.GetById(id);
            if (selectedCake == null)
            {
                return NotFound();
            }

            await _shoppingCart.RemoveFromCartAsync(selectedCake);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAllCart()
        {
            await _shoppingCart.ClearCartAsync();
            return RedirectToAction("Index");
        }
    }
}

