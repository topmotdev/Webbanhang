using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using WebApplication1.Data;
using WebApplication1.Models.EF;
using X.PagedList;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IWebHostEnvironment _env;
        public ProductCategoryController(ApplicationDbcontext dbcontext,IWebHostEnvironment env)
        {
            _dbcontext = dbcontext;
            _env = env;
        }
        public IActionResult Index(string SearchText,int? page)
        {
            int PageNumber = page ?? 1;
            int PageSize = 8;
            IQueryable<ProductCategory> items = _dbcontext.ProductCategories.OrderByDescending(x=>x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x=>x.Title != null && x.Title.Contains(SearchText));
            }
            int calculatedPageSize = (int)Math.Pow(2, (PageNumber - 1) * 3) + PageNumber;
            ViewBag.PageSize = calculatedPageSize;
            PagedList<ProductCategory> lst = new PagedList<ProductCategory>(items, PageNumber, PageSize);
            return View(lst);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ProductCategory item)
        {
            if (ModelState.IsValid)
            {
                item.CreatedDate = DateTime.Now;
                item.ModifiedrDate = DateTime.Now;
                _dbcontext.ProductCategories.Add(item);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            else 
                return View(item);
        }
    }
}
