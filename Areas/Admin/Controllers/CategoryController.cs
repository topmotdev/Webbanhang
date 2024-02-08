using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.EF;
using X.PagedList;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {

        private readonly ApplicationDbcontext _dbcontext;
        public CategoryController(ApplicationDbcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        
        public IActionResult Index(string SearchText,int?page)
        {
            int PageNumber = page ?? 1;
            int PageSize = 8;
            IQueryable<Category> items = _dbcontext.Categories.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Title != null && x.Title.Contains(SearchText));
            }
            ViewBag.PageSize = PageNumber + (int)Math.Pow(2, 3 * (PageNumber - 1));
            PagedList<Category> list = new PagedList<Category>(items, PageNumber, PageSize);


            return View(list);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                // Add category to the database
                category.CreatedDate = DateTime.Now;
                category.ModifiedrDate = DateTime.Now;
                _dbcontext.Categories.Add(category);
                _dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            // If ModelState is not valid, return to the Add view with validation errors
            return View(category);
        }
        public IActionResult Edit(int Id)
        {
            var category = _dbcontext.Categories.FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                return View("Error");
            }
             
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin của danh mục trong cơ sở dữ liệu
                _dbcontext.Entry(category).State = EntityState.Modified;
                category.CreatedDate= DateTime.Now;
                category.ModifiedrDate= DateTime.Now;
                _dbcontext.SaveChanges();

                return RedirectToAction("Index"); // Chuyển hướng sau khi chỉnh sửa thành công
            }

            // Nếu có lỗi kiểm tra mô hình, quay lại biểu mẫu chỉnh sửa để hiển thị lỗi
            return View(category);
        }
        public IActionResult Delete(int Id)

        {
            var category = _dbcontext.Categories.FirstOrDefault(c => c.Id == Id);
            if (category == null)
            {
                return View("Error");
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            if(category.Id < 0) {
                return BadRequest();
            }
            if(category==null)
            {
                return NotFound();
            }
            _dbcontext.Categories.Remove(category);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
