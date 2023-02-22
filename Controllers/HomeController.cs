using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BackFinal.Models;
using BackFinal.DAL;
using BackFinal.ViewModels;

namespace BackFinal.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        HomeVM homeVM = new HomeVM();
        homeVM.IndexSliders = _context.IndexSliders.ToList();
        homeVM.NoticeBoards = _context.NoticeBoards.ToList();
        homeVM.NoticeBoardRights = _context.NoticeBoardRights.ToList();
        homeVM.Questions = _context.Questions.ToList();
        homeVM.Offers = _context.Offers.ToList();
        homeVM.Testimonials = _context.Testimonials.ToList();
        homeVM.Blogs = _context.Blogs.ToList();
        return View(homeVM);

    }


}

