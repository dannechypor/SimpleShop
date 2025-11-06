using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Shop.BLL.Interfaces;

namespace ForGoodTime.Components;

public class CategorySummary(ICategoryService categoryService) : ViewComponent
{
    public Task<IViewComponentResult> InvokeAsync()
    {
        return Task.FromResult<IViewComponentResult>(View(new CategoryListViewModel
        {
            Categories = categoryService.GetAllCategory()
        }));
    }
}