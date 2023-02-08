using System;
using BackFinal.DAL;
using BackFinal.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackFinal.ViewComponents
{
    public class NoticeBoardViewComponent
    {
        private readonly AppDbContext _appDbContext;

        public NoticeBoardViewComponent(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<NoticeBoard> NoticeBoard = _appDbContext.NoticeBoards.ToList();
            return View(await Task.FromResult(NoticeBoard));
        }

        private IViewComponentResult View(List<NoticeBoard> noticeBoards)
        {
            throw new NotImplementedException();
        }
    }
}

