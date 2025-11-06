using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using Model.Models;

namespace ForGoodTime.Components;

public class TopSellingProductsSummary(IProductService productService) : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
        var top = productService.GetTopSellingProducts();

        return Task.FromResult<IViewComponentResult>(View(new TopSellingViewModel
        {
            Tops = top
        }));
    }
}