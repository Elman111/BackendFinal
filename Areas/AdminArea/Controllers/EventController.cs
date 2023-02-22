using BackFinal.DAL;
using BackFinal.Helpers.Extension;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace BackFinal.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class EventController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public EventController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;
        }

        public IActionResult Index()
        {
            var events = _appDbContext.Events.ToList();
            _appDbContext.SaveChanges();
            return View(events);
        }
        //[Authorize]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Event events)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }
            if (!events.Photo.CheckImage())
            {
                ModelState.AddModelError("Photo", "sekil sec");
            }
            if (events.Photo.CheckImageSize(1000))
            {
                ModelState.AddModelError("Photo", "olcu boyukdur");
            }



            Event newEvent = new Event();
            newEvent.ImageUrl = events.Photo.SaveImage(_env, "img/event");
            newEvent.Desc = events.Desc;
            newEvent.Title = events.Title;
            _appDbContext.Events.Add(newEvent);
            _appDbContext.SaveChanges();
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            Event events = _appDbContext.Events.Find(id);
            if (events == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath + "img" + events.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _appDbContext.Events.Remove(events);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null) return NotFound();
            Event events = _appDbContext.Events.Find(id);
            if (events == null) return NotFound();
            return View(new UpdateEventVM { ImageUrl = events.ImageUrl, Title = events.Title, Desc = events.Desc });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public IActionResult Update(int? id, UpdateEventVM events)
        {
            if (id == null) return NotFound();
            Event existEvent = _appDbContext.Events.Find(id);
            if (existEvent == null) return NotFound();
            string filename = null;

            if (events.Photo != null)
            {
                string path = Path.Combine(_env.WebRootPath, "img/event", existEvent.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                if (!events.Photo.CheckImage())
                {
                    ModelState.AddModelError("Photo", "sekil sec");
                }
                if (events.Photo.CheckImageSize(1000))
                {
                    ModelState.AddModelError("Photo", "olcu boyukdur");

                }
                filename = events.Photo.SaveImage(_env, "img/slider");

            }
            existEvent.ImageUrl = filename ?? existEvent.ImageUrl;
            existEvent.Desc = events.Desc;
            existEvent.Title = events.Title;
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
