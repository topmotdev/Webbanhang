using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            IQueryable<Product> items = _dbcontext.Products.OrderBy(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Title != null && x.Title.Contains(SearchText));
            }
            int calculatedPageSize = (int)Math.Pow(2, (pageNumber - 1) * 3) + pageNumber;
            ViewBag.PageSize = calculatedPageSize;
            ViewBag.ProductCategory = new SelectList(_dbcontext.ProductCategories.ToList(), "Id", "Title");
            PagedList <Product> lst = new PagedList<Product>(items, pageNumber, pageSize);
            return View(lst);
            
        }
        public IActionResult Add() 
        {
            ViewBag.ProductCategory = new SelectList(_dbcontext.ProductCategories.ToList(), "Id", "Title");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Product product)
        {
            
            if (ModelState.IsValid)
            {
                product.IsHot = Request.Form["IsHot"].Contains("true");
                product.IsHome = Request.Form["IsHome"].Contains("true");
                product.IsFeature = Request.Form["IsFeature"].Contains("true");
                product.IsSale = Request.Form["IsSale"].Contains("true");
                product.CreatedDate = DateTime.Now;
                product.ModifiedrDate = DateTime.Now;
                
                _dbcontext.Products.Add(product);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbcontext.ProductCategories.ToList(), "Id", "Title");
            return View(product);
        }
        public IActionResult Edit(int Id)
        {
            ViewBag.ProductCategory = new SelectList(_dbcontext.ProductCategories.ToList(), "Id", "Title");
            var item = _dbcontext.Products.FirstOrDefault(x => x.Id == Id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product item)
        {
            if(ModelState.IsValid && item != null)
            {
                item.IsHot = Request.Form["IsHot"].Contains("true");
                item.IsHome = Request.Form["IsHome"].Contains("true");
                item.IsFeature = Request.Form["IsFeature"].Contains("true");
                item.IsSale = Request.Form["IsSale"].Contains("true");
                item.CreatedDate = DateTime.Now;
                item.ModifiedrDate = DateTime.Now;
                _dbcontext.Entry(item).State = EntityState.Modified;
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(item);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var item = _dbcontext.Products.FirstOrDefault(x => x.Id == Id);
            if(item != null)
            {
                _dbcontext.Products.Remove(item);
                _dbcontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult Delete_selected(List<int> Ids)
        {
            if (Ids != null && Ids.Count > 0)
            {
                // Lặp qua danh sách ID và xóa các bản ghi tương ứng
                foreach (int Id in Ids)
                {
                    // Viết mã xóa bản ghi có ID là 'id' ở đây
                    var item = _dbcontext.Products.FirstOrDefault(c => c.Id == Id);
                    if (item != null)
                    {
                        _dbcontext.Products.Remove(item);
                        _dbcontext.SaveChanges();
                    }
                }

                // Trả về một phản hồi thành công hoặc dữ liệu khác nếu cần
                return Json(new { success = true, message = "Xóa thành công các bản ghi." });
            }
            else
            {
                return Json(new { success = false, message = "Xóa thất bại." });
            }
        }
    }
}
