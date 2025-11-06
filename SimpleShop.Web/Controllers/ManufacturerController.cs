using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Model.ViewModels;

namespace SimpleShop.Web.Controllers;

public class ManufacturerController(IManufacturerService manufacturerService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View(manufacturerService.GetAllManufacturer());
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ManufacturerViewModel model)
    {
        await manufacturerService.AddManufacturer(model.Name);
        return RedirectToAction("Index", "Manufacturer");
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0) return BadRequest();
        var result = await manufacturerService.Delete(id);
        if (result.Succedeed) return RedirectToAction("Index", "Manufacturer");

        return BadRequest();
    }
}