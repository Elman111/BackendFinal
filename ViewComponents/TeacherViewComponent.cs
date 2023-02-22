using BackFinal.DAL;
using BackFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackFinal.ViewComponents
{
    public class TeacherViewComponent : ViewComponent
    {
        private readonly AppDbContext _appDbContext;
        public TeacherViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Teachers> Teachers = _appDbContext.Teachers.Take(4).ToList();
            return View(await Task.FromResult(Teachers));
        }

    }
}
