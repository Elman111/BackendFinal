using System;
using BackFinal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackFinal.DAL
{
    
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        internal readonly object Category;

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

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<TeacherAreaSocial> TeacherAreaSocials { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Category> Categories { get; internal set; }

        public DbSet<Latest_From_Blog> Latest_From_Blogs { get; internal set; }

        public DbSet<Teachers> Teachers { get; internal set; }
        public object Sliders { get; internal set; }
        public object Latest_From_Blog { get; internal set; }
    }

}

