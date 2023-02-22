using BackFinal.DAL;
using BackFinal.Helpers.Extension;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;

namespace BackFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]


    public class SliderController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }
        
        public IActionResult Index()
        {
            var sliders = _appDbContext.Sliders.ToList();
           _appDbContext.SaveChanges();
            return View(sliders);
        }
        //[Authorize]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!slider.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "sekil sec");
            }
            if (slider.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "olcu boyukdur");
            }



            Slider newSlider = new Slider();
            newSlider.ImageUrl = slider.Photo.SaveImage(_env, "img/slider");
            newSlider.Desc = slider.Desc;
            newSlider.Title = slider.Title;
            _appDbContext.Sliders.Add(newSlider);
            _appDbContext.SaveChanges();
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = _appDbContext.Sliders.Find(id);
            if (slider == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath + "img" + slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _appDbContext.Sliders.Remove(slider);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = _appDbContext.Sliders.Find(id);
            if (slider == null) return NotFound();
            return View(new UpdateSliderVM { ImageUrl = slider.ImageUrl,Title=slider.Title,Desc=slider.Desc });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(int? id, UpdateSliderVM slider)
        {
            if (id == null) return NotFound();
            Slider existSlider = _appDbContext.Sliders.Find(id);
            if (existSlider == null) return NotFound();
            string filename = null;

            if (slider.Photo != null)
            {
                string path = Path.Combine(_env.WebRootPath,"img/slider", existSlider.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (!slider.Photo.CheckImage())
                {
                    ModelState.AddModelError("Photo", "sekil sec");
                }
                if (slider.Photo.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "olcu boyukdur");

                }
                filename = slider.Photo.SaveImage(_env, "img/slider");

            }
            existSlider.ImageUrl = filename ?? existSlider.ImageUrl;
            existSlider.Desc = slider.Desc;
            existSlider.Title = slider.Title;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");


        }

    }
}