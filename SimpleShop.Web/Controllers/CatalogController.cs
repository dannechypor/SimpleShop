using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using Model.Entities;
using Model.Models;
using Shop.BLL.Interfaces;
using Shop.DAL;
using Shop.DAL.EF;

namespace ForGoodTime.Controllers;

public class CatalogController(
    ICatalogService catalogService,
    IReviewService reviewService,
    ApplicationDbContext context,
    IMapper mapper)
    : Controller
{
    private ICatalogService CatalogService { get; } = catalogService;
    private IReviewService ReviewService { get; } = reviewService;

    [HttpGet]
    public async Task<IActionResult> Index(int id, int? pr, string name, FilterModel model,
        SortState sortOrder = SortState.NameAsc, int page = 1)
    {
        var category = CatalogService.GetAllCategory().ToList();
        var manufacturers = CatalogService.GetAllManufacturer().ToList();
        var product = new ProductDTO();
        IEnumerable<Product> Items = new List<Product>();
        const int pageSize = 6;

        var source = context.Products.Where(x => x.CategoryId == id);
        if (pr != null && pr != 0) source = source.Where(p => p.Id == pr);
        if (!string.IsNullOrEmpty(name)) source = source.Where(p => p.Name.Contains(name));

        source = sortOrder switch
        {
            SortState.NameDesc => source.OrderByDescending(s => s.Name),
            SortState.PriceAsc => source.OrderBy(s => s.Price),
            SortState.PriceDesc => source.OrderByDescending(s => s.Price),
            _ => source.OrderBy(s => s.Name)
        };

        product.CategoryId = id;
        if (id == 0) source = context.Products;
        var count = source.Count();
        var items = source.Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();

        var pageViewModel = new PageViewModel(count, page, pageSize);
        var viewModel = new IndexViewModel<Product>
        {
            SortViewModel = new SortViewModel(sortOrder),
            FilterViewModel = new FilterViewModel(context.Products.ToList(), pr, name),
            PageViewModel = pageViewModel,
            Products = items
        };
        return View(new CatalogProductListViewModel
        {
            FilterModel = model,
            Product = product,
            IndexViewModel = viewModel,
            Categories = category,
            Manufacturers = manufacturers
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
        var review = mapper.Map<ReviewViewModel, ReviewDTO>(model.Review);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await ReviewService.Create(review, userId);

        return RedirectToActionPermanent("Details");
    }
}