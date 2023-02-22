 using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackFinal.Models;
using BackFinal.DAL;
using BackFinal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BackFinal.Controllers;

public class AboutController : Controller
{
    private readonly AppDbContext _context;

    public AboutController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        AboutVM aboutVM = new AboutVM();
        aboutVM.AboutAreas = _context.AboutAreas.FirstOrDefault();
        aboutVM.TeacherAreas = _context.TeacherAreas.ToList();
        aboutVM.AboutTestimonials = _context.AboutTestimonials.ToList();
        aboutVM.TeacherAreaSocials = _context.TeacherAreaSocials.Include(t=>t.TeacherArea).FirstOrDefault();

        return View(aboutVM);

    }


}

