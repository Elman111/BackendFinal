using System;
using BackFinal.Models;
using Microsoft.EntityFrameworkCore;

namespace BackFinal.DAL
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<IndexSlider> IndexSliders { get; set; }

        public DbSet<NoticeBoard> NoticeBoards { get; set; }

        public DbSet<NoticeBoardRight> NoticeBoardRights { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Testimonial> Testimonials { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<AboutArea> AboutAreas { get; set; }

        public DbSet<TeacherArea> TeacherAreas { get; set; }

        public DbSet<AboutTestimonial> AboutTestimonials { get; set; }
    }

}

