using BackFinal.DAL;
using BackFinal.Helpers.Extension;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace BackFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            var blogs = _appDbContext.Latest_From_Blogs.ToList();

          
            _appDbContext.SaveChanges();
            return View(blogs);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Latest_From_Blog blogs)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!blogs.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "sekil sec");
            }
            if (blogs.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "olcu boyukdur");
            }



            Latest_From_Blog newLatest_From_Blog = new Latest_From_Blog();
            newLatest_From_Blog.ImageUrl = blogs.Photo.SaveImage(_env, "img/blog");
            newLatest_From_Blog.Desc = blogs.Desc;
            _appDbContext.Latest_From_Blogs.Add(newLatest_From_Blog);
            _appDbContext.SaveChanges();
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Latest_From_Blog blogs = _appDbContext.Latest_From_Blog.Find(id);
            if (blogs == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath + "img" + blogs.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _appDbContext.Latest_From_Blog.Remove(blogs);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Latest_From_Blog blogs = _appDbContext.Latest_From_Blog.Find(id);
            if (blogs == null) return NotFound();
            return View(new UpdateBlogVM { ImageUrl = blogs.ImageUrl, Desc = blogs.Desc });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(int? id, UpdateBlogVM blogs)
        {
            if (id == null) return NotFound();
            Latest_From_Blog existLatest_From_Blog = _appDbContext.Latest_From_Blog.Find(id);
            if (existLatest_From_Blog == null) return NotFound();
            string filename = null;

            if (blogs.Photo != null)
            {
                string path = Path.Combine(_env.WebRootPath, "img/blog", existLatest_From_Blog.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (!blogs.Photo.CheckImage())
                {
                    ModelState.AddModelError("Photo", "sekil sec");
                }
                if (blogs.Photo.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "olcu boyukdur");

                }
                filename = blogs.Photo.SaveImage(_env, "img/blog");

            }
            existLatest_From_Blog.ImageUrl = filename ?? existLatest_From_Blog.ImageUrl;
            existLatest_From_Blog.Desc = blogs.Desc;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
