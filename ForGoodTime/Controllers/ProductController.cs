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
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForGoodTime.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService ProductService;

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }


        public IActionResult Index()
        {
            return View(ProductService.GetAllProduct());
        }


        [HttpGet]
        public  IActionResult Add()
        {
            return View(new AddProductViewModel
            {
                Categories = ProductService.GetAllCategory(),
                Manufacturers = ProductService.GetAllManufacturer()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
             
               await ProductService.AddProduct(new ProductDTO
               { Name = model.Product.Name,
                   LongDescription = model.Product.LongDescription,
                   ShortDescription = model.Product.ShortDescription,
                   Date = DateTime.UtcNow,
                  
                   Hight = model.Product.Higth,
                   Width = model.Product.Width,
                   Deepth = model.Product.Deepth,
                   Price = model.Product.Price,
                   ImageUrl_1 = model.Product.ImageUrl_1,
                   ImageUrl_2 = model.Product.ImageUrl_2,
                   ImageUrl_3 = model.Product.ImageUrl_3,
                   CategoryId = model.Product.CategoryId,
                   ManufacturerId = model.Product.ManufacturerId });

          return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                OperationDetails result = await ProductService.Delete(id);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return BadRequest();
        }
    }
}