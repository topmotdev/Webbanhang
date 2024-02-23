using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbcontext   _dbcontext;

        public HomeController(ILogger<HomeController> logger,ApplicationDbcontext dbcontext)
        {
            _logger = logger;
            _dbcontext = dbcontext;
        }
		
		public IActionResult Index()
        {

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult MenuTop()
        {
            List<Models.EF.Category> items = _dbcontext.Categories.OrderBy(x=>x.Id).ToList();
            return PartialView("_Menutop",items);
        }
		public IActionResult MenuProduct()
		{
            List<Models.EF.Product> items = _dbcontext.Products.OrderBy(x=>x.Id).ToList();
            items= items.Where(x=>x.ProductCategoryID ==1).ToList();

            return PartialView("_MenuProduct", items);
		}
	}
}
