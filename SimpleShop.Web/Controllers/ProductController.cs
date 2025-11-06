using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Models;
using Model.ViewModels;
using Shop.BLL.Interfaces;

namespace ForGoodTime.Controllers;

public class ProductController(
    IProductService productService,
    ICategoryService categoryService,
    IManufacturerService manufacturerService) : Controller
{
    public IActionResult Index()
    {
        return View(productService.GetAllProduct());
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View(new AddProductViewModel
        {
            Categories = categoryService.GetAllCategory(),
            Manufacturers = manufacturerService.GetAllManufacturer()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProductViewModel model)
    {
        await productService.AddProduct(new ProductDto
        {
            Name = model.Product.Name,
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
            ManufacturerId = model.Product.ManufacturerId
        });

        return RedirectToAction("Index", "Product");
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return BadRequest();
        var result = await productService.Delete(id);
        if (result.Succedeed) return RedirectToAction("Index", "Product");

        return BadRequest();
    }
}