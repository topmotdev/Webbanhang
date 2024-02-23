using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using WebApplication1.Data;
using WebApplication1.Models.EF;
namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbcontext _dbcontext;

        public ProductController(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        [Route("/san-pham")]
        public IActionResult Index()
        {
            List<Product> items = _dbcontext.Products.ToList();
            return View(items);
        }
		[Route("/san-pham/{Id}")]
		public IActionResult Details(int Id)
        {
            var item = _dbcontext.Products.FirstOrDefault(p => p.Id == Id);
            return View(item);
        }
    }
}
