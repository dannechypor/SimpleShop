using Microsoft.AspNetCore.Mvc;
using ForGoodTime.Components;
using Shop.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForGoodTime.Views.Shared.Components.ShoppingCartSummary;
using Shop.DAL;
using Model.Models;
using AutoMapper;
using Model.Entities;
using Shop.DAL.Repositories;
using Shop.DAL.Interfaces;

namespace ForGoodTime.Components
{
    public class TopSellingProductsSummary : ViewComponent
    {
        private readonly IProductService ProductService;
        private readonly IMapper _mapper;

        public TopSellingProductsSummary( IMapper mapper,IProductService productService)
        {
            ProductService = productService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var top = ProductService.GetTopSellings();

            return View(new TopSellingViewModel
            {
                Tops = top
            }); ;
        }
    }
}
