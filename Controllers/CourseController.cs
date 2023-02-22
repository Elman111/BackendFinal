using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackFinal.Models;
using BackFinal.DAL;
using BackFinal.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BackFinal.Controllers;

public class CourseController : Controller
{
    private readonly AppDbContext _context;

    public CourseController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        CourseVM courseVM = new CourseVM();
        courseVM.Courses = _context.Courses.ToList();

        return View(courseVM);

    }


}