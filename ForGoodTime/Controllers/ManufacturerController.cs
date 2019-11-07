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
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService ManufacturerService;

        public ManufacturerController(IManufacturerService manufacturerService)
        {
            ManufacturerService = manufacturerService;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Index()
        {
            return View(ManufacturerService.GetAllManufacturer());
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult>  Add(ManufacturerViewModel model)
        {
            await ManufacturerService.AddManufacturer(model.Name);
            return RedirectToAction("Index", "Manufacturer");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id != 0)
            {
                OperationDetails result = await ManufacturerService.Delete(id);
                if (result.Succedeed)
                {
                    return RedirectToAction("Index", "Manufacturer");
                }
            }
            return BadRequest();
        }
    }
}