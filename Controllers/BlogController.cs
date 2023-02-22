using BackFinal.DAL;
using BackFinal.Models;
using BackFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection.Metadata;

namespace BackFinal.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1, int take = 3)
        {
            var blogs = _context.Latest_From_Blogs.ToList();


            var blogCount = blogs.Count();

            var items = blogs.Skip((page - 1) * take).Take(take).ToList();
            PaginationVM<Latest_From_Blog> pagination = new PaginationVM<Latest_From_Blog>(page, CalculatePageCount(blogCount, take), items);

            return View(pagination);
        }

        private int CalculatePageCount(int count, int take)
        {
            return (int)Math.Ceiling((decimal)count / take);
        }
    }
}
