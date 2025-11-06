using Microsoft.AspNetCore.Mvc;
using Model.ViewModels;
using Shop.BLL.Interfaces;

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