﻿using BackFinal.DAL;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BackProject.Controllers
{
    public class EventController : Controller
    {

        private readonly AppDbContext _context;

        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var events= _context.Events.ToList();
            return View(events);
        }
    }
}
