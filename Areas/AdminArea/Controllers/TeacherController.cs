using BackFinal.DAL;
using BackFinal.Helpers.Extension;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            var teachers = _appDbContext.Teachers.ToList();
            _appDbContext.SaveChanges();
            return View(teachers);
        }
        //[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Teachers teacher)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!teacher.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "sekil sec");
            }
            if (teacher.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "olcu boyukdur");
            }



            Teachers newTeacher = new Teachers();
            newTeacher.ImageUrl = teacher.Photo.SaveImage(_env, "img/teacher");
            newTeacher.Desc = teacher.Desc;
            newTeacher.Name = teacher.Name;
            newTeacher.Facebook = teacher.Facebook;
            newTeacher.Instagram = teacher.Instagram;
            newTeacher.LinkedIn = teacher.LinkedIn;
            newTeacher.Whatsapp = teacher.Whatsapp;
            newTeacher.Degree = teacher.Degree;
            newTeacher.Email = teacher.Email;
            newTeacher.Experience = teacher.Experience;
            newTeacher.Faculty = teacher.Faculty;
            newTeacher.Hobby = teacher.Hobby;
            _appDbContext.Teachers.Add(newTeacher);
            _appDbContext.SaveChanges();
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Teachers teacher = _appDbContext.Teachers.Find(id);
            if (teacher == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath + "img" + teacher.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _appDbContext.Teachers.Remove(teacher);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Teachers teacher = _appDbContext.Teachers.Find(id);
            if (teacher == null) return NotFound();
            return View(new UpdateTeacherVM { ImageUrl = teacher.ImageUrl, Name = teacher.Name, Desc = teacher.Desc });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(int? id, UpdateTeacherVM teacher)
        {
            if (id == null) return NotFound();
            Teachers existTeacher = _appDbContext.Teachers.Find(id);
            if (existTeacher == null) return NotFound();
            string filename = null;

            if (teacher.Photo != null)
            {
                string path = Path.Combine(_env.WebRootPath, "img/teacher", existTeacher.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (!teacher.Photo.CheckImage())
                {
                    ModelState.AddModelError("Photo", "sekil sec");
                }
                if (teacher.Photo.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "olcu boyukdur");

                }
                filename = teacher.Photo.SaveImage(_env, "img/teacher");

            }
            existTeacher.ImageUrl = filename ?? existTeacher.ImageUrl;
            existTeacher.Desc = teacher.Desc;
            existTeacher.Name = teacher.Name;
            existTeacher.Facebook = teacher.Facebook;
            existTeacher.Instagram = teacher.Instagram;
            existTeacher.LinkedIn = teacher.LinkedIn;
            existTeacher.Whatsapp = teacher.Whatsapp;
            existTeacher.Degree = teacher.Degree;
            existTeacher.Email = teacher.Email;
            existTeacher.Experience = teacher.Experience;
            existTeacher.Faculty = teacher.Faculty;
            existTeacher.Hobby = teacher.Hobby;



            _appDbContext.SaveChanges();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Detail(int?id)
        {
            if (id == null) return NotFound();

            Teachers teacher = await _appDbContext.Teachers.FindAsync(id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }

    }
}
