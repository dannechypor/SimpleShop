using Microsoft.AspNetCore.Mvc;
using SimpleShop.BLL.Interfaces;
using SimpleShop.Model.ViewModels;

namespace SimpleShop.Web.Components;

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