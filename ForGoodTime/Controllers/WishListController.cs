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
    public class WishListController : Controller
    {
        private readonly IWishListService wishListService;
        private IMapper mapper;
        public WishListController(IWishListService _wishListService, IMapper _mapper)
        {
            wishListService = _wishListService;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AddToWhishList(WishListItemViewModel model, int id)
        {
            var wishList = mapper.Map<WishListItemViewModel, WishListDTO>(model);
            wishList.ProductId = id;
            wishList.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await wishListService.AddToWhishList(wishList);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {            
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var products = wishListService.GetAllProductIdByUser(userId);

            return View(new MyWishListViewModel
            {
                Products = products

            });

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != 0)
            {
                OperationDetails result = await wishListService.DeleteRelationship(id, userId);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
        }
    }
}
