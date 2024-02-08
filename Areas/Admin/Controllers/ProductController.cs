using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.EF;
using X.PagedList;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbcontext  _dbcontext;
        private readonly IWebHostEnvironment _environment;
        public ProductController(ApplicationDbcontext dbcontext, IWebHostEnvironment environment)
        {
            _dbcontext = dbcontext;
            _environment = environment;
        }

        public IActionResult Index(string SearchText, int? page)
        {

            int pageSize = 8;
            int pageNumber = page ?? 1;
            IQueryable<Product> items = _dbcontext.Products.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Title != null && x.Title.Contains(SearchText));
            }
            int calculatedPageSize = (int)Math.Pow(2, (pageNumber - 1) * 3) + pageNumber;
            ViewBag.PageSize = calculatedPageSize;
            PagedList<Product> lst = new PagedList<Product>(items, pageNumber, pageSize);
            return View(lst);

        }
        public IActionResult Add() {
            return View();
        }
    }
}
