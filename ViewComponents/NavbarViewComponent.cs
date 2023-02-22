using BackFinal.DAL;
using BackFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BackFinal.ViewComponents
{
    public class NavbarViewComponent:ViewComponent
    {
       
            private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public NavbarViewComponent(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    List<Navbar> Navbar = _context.Navbar.ToList();
        //    return View(await Task.FromResult(Navbar));
        //}

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Navbar nav = _context.Navbar.FirstOrDefault();
            ViewBag.FullName = "";

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.FullName = user.FullName;
            }

            return View(await Task.FromResult(nav));
        }

    }
    
}
