using BackFinal.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var teachers= _context.Teachers.ToList();
            return View(teachers);
        }
    }
}
