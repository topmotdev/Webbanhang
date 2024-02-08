
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.EF;
using X.PagedList;
using System;
namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbcontext _dbcontext;
        private readonly IWebHostEnvironment _env;
        public PostsController(ApplicationDbcontext dbcontext, IWebHostEnvironment env)
        {
            _dbcontext = dbcontext;
            _env = env;
        }
        public IActionResult Index(string SearchText,int? page)
        {
            
            int pageSize = 8;
            int pageNumber = page ?? 1;
            IQueryable<Post> items = _dbcontext.Posts.OrderByDescending(x=>x.Id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Title != null && x.Title.Contains(SearchText));
            }
            int calculatedPageSize = (int)Math.Pow(2, (pageNumber - 1) * 3) + pageNumber;
            ViewBag.PageSize = calculatedPageSize;
            PagedList<Post> lst = new PagedList<Post>(items, pageNumber, pageSize);
            return View(lst);

        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Add(Post item)
        {
            if (ModelState.IsValid)
            {
                // Add category to the database
                item.CreatedDate = DateTime.Now;
                item.ModifiedrDate = DateTime.Now;
                _dbcontext.Posts.Add(item);
                _dbcontext.SaveChanges();

                return RedirectToAction("Index");
            }

            // If ModelState is not valid, return to the Add view with validation errors
            return View(item);
        }
        public IActionResult UploadImage(List<IFormFile> files)
        {
            var filepath = "";
            foreach (IFormFile photo in Request.Form.Files)
            {
                string severMapPath = Path.Combine(_env.WebRootPath, "Image", photo.FileName);
                using (var stream = new FileStream(severMapPath, FileMode.Create))
                {
                    photo.CopyTo(stream);
                }
                filepath = "https://localhost:7141/" + "Image/" + photo.FileName;
            }
            return Json(new { url = filepath });

        }
        public IActionResult Edit(int Id)
        {
            var item = _dbcontext.Posts.FirstOrDefault(x => x.Id == Id);
            if (item == null)
            {
                return View("Error");
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post item)
        {
            if (item == null)
            {
                return View("Error");
            }
            _dbcontext.Entry(item).State = EntityState.Modified;
            item.CreatedDate = DateTime.Now;
            item.ModifiedrDate = DateTime.Now;
            _dbcontext.Posts.Add(item);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var item = _dbcontext.Posts.FirstOrDefault(c => c.Id == Id);
            if (item != null)
            {
                _dbcontext.Posts.Remove(item);
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
                    var item = _dbcontext.Posts.FirstOrDefault(c => c.Id == Id);
                    if (item != null)
                    {
                        _dbcontext.Posts.Remove(item);
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
