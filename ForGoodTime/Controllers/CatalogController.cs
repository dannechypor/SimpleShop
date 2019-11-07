using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Shop.BLL.Infrastructure;
using Shop.BLL.Interfaces;
using Shop.BLL.Services;
using Shop.DAL;
using Shop.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ForGoodTime.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private  ICatalogService CatalogService { get; set; }
        private IReviewService ReviewService { get; set; }
        private readonly IMapper _mapper;
        public CatalogController(ICatalogService catalogService, IReviewService reviewService, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            CatalogService = catalogService;
            ReviewService = reviewService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id, int? pr, string name, FilterModel model,
            SortState sortOrder = SortState.NameAsc,  int page = 1)
        {
            
           // var product = CatalogService.GetProductByCategoryId(id);
            var category = CatalogService.GetAllCategory().ToList();
            var manufacturers = CatalogService.GetAllManufacturer().ToList();
            ProductDTO product = new ProductDTO();
            IEnumerable<Product> Items = new List<Product>();
            int pageSize = 6;   // количество элементов на странице

            IQueryable<Product> source = _context.Products.Where(x => x.CategoryId == id);
            if (pr != null && pr != 0)
            {
                source = source.Where(p => p.Id == pr);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }
           
            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.PriceAsc:
                    source = source.OrderBy(s => s.Price);
                    break;
                case SortState.PriceDesc:
                    source = source.OrderByDescending(s => s.Price);
                    break;
                default:
                    source = source.OrderBy(s => s.Name);
                    break;
            }

            product.CategoryId = id;
            if (id == 0) {
                source = _context.Products;
            }
            var count =  source.Count();
            var items =  source.Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<Product> viewModel = new IndexViewModel<Product>
            {
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(_context.Products.ToList(), pr, name),
                PageViewModel = pageViewModel,
                Products = items
            };
            return View(new CatalogProductListViewModel {
                FilterModel=model,
                Product = product,
                IndexViewModel= viewModel,
                Categories = category,
                Manufacturers = manufacturers
            });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = CatalogService.GetProductById(id);
            ReviewViewModel model = new ReviewViewModel();
           

            return View(new CatalogProductListViewModel
            {
                Product = product,
                Review = model
                

            });
        }

        [HttpPost]
        public async Task<IActionResult> Details(CatalogProductListViewModel model)
        {
            var review = _mapper.Map<ReviewViewModel, ReviewDTO>(model.Review);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await ReviewService.Create(review, userId);

            return RedirectToActionPermanent("Details");

        }

    }
}
