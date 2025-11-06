using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.ViewModels;
using Shop.BLL.Interfaces;

namespace ForGoodTime.Controllers;

public class CategoryController(ICategoryService categoryService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(new CategoryListViewModel
        {
            Categories = categoryService.GetAllCategory()
        });
    }

    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(CategoryViewModel model)
    {
        await categoryService.AddCategory(model.Name);
        return RedirectToAction("Index", "Category");
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return BadRequest();
        var result = await categoryService.Delete(id);
        if (result.Succedeed) return RedirectToAction("Index", "Category");

        return BadRequest();
    }
}