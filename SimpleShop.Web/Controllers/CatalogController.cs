using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Model.ViewModels;
using Shop.BLL.Interfaces;
using Shop.DAL;
using SimpleShop.BLL.Interfaces;
using SimpleShop.DAL.EF;
using SimpleShop.Model.Entities;

namespace ForGoodTime.Controllers;

public class CatalogController(
    ICatalogService catalogService,
    IReviewService reviewService,
    ICategoryService categoryService,
    IManufacturerService manufacturerService,
    ApplicationDbContext context,
    IMapper mapper)
    : Controller
{
    private ICatalogService CatalogService { get; } = catalogService;
    private IReviewService ReviewService { get; } = reviewService;

    [HttpGet]
    public async Task<IActionResult> Index(
        int? categoryId,
        int? manufacturerId,
        string search,
        decimal? minPrice,
        decimal? maxPrice,
        FilterModel model,
        SortState sortOrder = SortState.NameAsc,
        int page = 1,
        int pageSize = 12)
    {
        var categories = categoryService.GetAllCategory().ToList();
        var manufacturers = manufacturerService.GetAllManufacturer().ToList();
        var product = new ProductDto();
        
        // Build base query with includes
        var source = context.Products
            .Include(p => p.Category)
            .Include(p => p.Manufacturer)
            .AsQueryable();

        // Apply filters
        if (categoryId.HasValue && categoryId.Value > 0)
        {
            source = source.Where(p => p.CategoryId == categoryId.Value);
        }
        if (manufacturerId.HasValue && manufacturerId.Value > 0)
        {
            source = source.Where(p => p.ManufacturerId == manufacturerId.Value);
        }
        if (!string.IsNullOrWhiteSpace(search))
        {
            source = source.Where(p => p.Name.Contains(search));
        }
        if (minPrice.HasValue)
        {
            source = source.Where(p => p.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            source = source.Where(p => p.Price <= maxPrice.Value);
        }

        // Apply sorting
        source = sortOrder switch
        {
            SortState.NameDesc => source.OrderByDescending(s => s.Name),
            SortState.PriceAsc => source.OrderBy(s => s.Price),
            SortState.PriceDesc => source.OrderByDescending(s => s.Price),
            _ => source.OrderBy(s => s.Name)
        };

        // Get total count before pagination
        var count = source.Count();
        
        // Apply pagination
        var items = source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Calculate product counts for categories and manufacturers
        // For "All" count, we need to count all products without category/manufacturer filters
        // but with other filters (search, price) applied
        var baseFilteredQuery = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            baseFilteredQuery = baseFilteredQuery.Where(p => p.Name.Contains(search));
        }
        if (minPrice.HasValue)
        {
            baseFilteredQuery = baseFilteredQuery.Where(p => p.Price >= minPrice.Value);
        }
        if (maxPrice.HasValue)
        {
            baseFilteredQuery = baseFilteredQuery.Where(p => p.Price <= maxPrice.Value);
        }
        // Count for "All Products" - without category/manufacturer filters
        var allProductsFilteredCount = baseFilteredQuery.Count();
        
        var categoryCounts = categories.ToDictionary(
            c => c.Id,
            c => baseFilteredQuery.Count(p => p.CategoryId == c.Id)
        );
        
        var manufacturerCounts = manufacturers.ToDictionary(
            m => m.Id,
            m => baseFilteredQuery.Count(p => p.ManufacturerId.HasValue && p.ManufacturerId.Value == m.Id)
        );

        product.CategoryId = categoryId ?? 0;
        
        // Get min and max prices for slider
        var minPriceValue = context.Products.Min(p => (decimal?)p.Price) ?? 0;
        var maxPriceValue = context.Products.Max(p => (decimal?)p.Price) ?? 1000;
        
        var pageViewModel = new PageViewModel(count, page, pageSize);
        var viewModel = new IndexViewModel<Product>
        {
            SortViewModel = new SortViewModel(sortOrder),
            FilterViewModel = new FilterViewModel(context.Products.ToList(), manufacturerId, search),
            PageViewModel = pageViewModel,
            Products = items
        };
        
        return View(new CatalogProductListViewModel
        {
            FilterModel = model,
            Product = product,
            IndexViewModel = viewModel,
            Categories = categories,
            Manufacturers = manufacturers,
            CategoryCounts = categoryCounts,
            ManufacturerCounts = manufacturerCounts,
            CurrentCategoryId = categoryId,
            CurrentManufacturerId = manufacturerId,
            CurrentSearch = search,
            CurrentMinPrice = minPrice,
            CurrentMaxPrice = maxPrice,
            CurrentSortOrder = sortOrder,
            AllProductsCount = allProductsFilteredCount,
            MinPriceValue = minPriceValue,
            MaxPriceValue = maxPriceValue
        });
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = CatalogService.GetProductById(id);
        var model = new ReviewViewModel();


        return View(new CatalogProductListViewModel
        {
            Product = product,
            Review = model
        });
    }

    [HttpPost]
    public async Task<IActionResult> Details(CatalogProductListViewModel model)
    {
        var review = mapper.Map<ReviewViewModel, ReviewDto>(model.Review);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await ReviewService.Create(review, userId);

        return RedirectToActionPermanent("Details");
    }
}