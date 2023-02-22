using BackFinal.DAL;
using BackFinal.Helpers.Extension;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public CategoryController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            var categories = _appDbContext.Category.ToList();
            _appDbContext.SaveChanges();

            return View(categories);
        }

     
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View();

            var isExist = _appDbContext.Category
                .Any(c => c.Name.ToLower() == category.Name.ToLower());

            if (isExist)
            {
                ModelState.AddModelError("Name", "this name already exist");
                return View();
            }


            _appDbContext.Category.Add(category);

            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Category.Find(id);
            if (category == null) return NotFound();

            //string path = Path.Combine(_env.WebRootPath + "/img" + category.ImageUrl);
            //if (System.IO.File.Exists(path))
            //{
            //    System.IO.File.Delete(path);
            //}

            _appDbContext.Category.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Category category = _appDbContext.Category.Find(id);
            if (category == null) return NotFound();
            return View(new UpdateCategoryVM {  Name = category.Name });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(int? id, UpdateCategoryVM category)
        {
            if (id == null) return NotFound();
            Category existCategory = _appDbContext.Category.Find(id);
            if (existCategory == null) return NotFound();
            //string filename = null;

            //if (course.Photo != null)
            //{
            //    string path = Path.Combine(_env.WebRootPath, "img/course", existCourse.ImageUrl);
            //    if (System.IO.File.Exists(path))
            //    {
            //        System.IO.File.Delete(path);
            //    }

            //    if (!course.Photo.CheckImage())
            //    {
            //        ModelState.AddModelError("Photo", "sekil sec");
            //    }
            //    if (course.Photo.CheckImageSize(1000))
            //    {
            //        ModelState.AddModelError("Photo", "olcu boyukdur");

            //    }
            //    filename = course.Photo.SaveImage(_env, "img/course");

            //}
            //existCategory.ImageUrl = filename ?? existCategory.ImageUrl;
            //existCategory.Desc = course.Desc;
            existCategory.Name = category.Name;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");


        }


    }
}
