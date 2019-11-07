using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForGoodTime.Components
{
    public class CategorySummary : ViewComponent
    {
        private readonly ICategoryService CategoryService;

        public CategorySummary(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

       
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
           return View(new CategoryListViewModel
            {
                Categories = CategoryService.GetAllCategory()

            });
        }
    }
}
