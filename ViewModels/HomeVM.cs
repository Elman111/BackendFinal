using System;
using BackFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackFinal.ViewModels
{
    public class HomeVM
    {
        public List<IndexSlider> IndexSliders { get; set; }

        public List<NoticeBoard> NoticeBoards { get; set; }

        public List<NoticeBoardRight> NoticeBoardRights { get; set; }

        public List<Question> Questions { get; set; }

        public List<Offer> Offers { get; set; }

        public List<Event> Events { get; set; }

        public List<Testimonial> Testimonials { get; set; }

        public List<Blog> Blogs { get; set; }


    }
}

