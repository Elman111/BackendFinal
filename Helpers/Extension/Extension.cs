

using BackFinal.DAL;
using BackFinal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;

namespace BackFinal.Helpers.Extension
{
    public static class Extension
    {
        public static bool CheckImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
        public static bool CheckImageSize(this IFormFile file,int size)
        {
            return file.Length / 1024 > size;
        }

        public static string SaveImage(this IFormFile file, IWebHostEnvironment _env,string folder)
        {
            string fileName = Guid.NewGuid() + file.FileName;
            string path = Path.Combine(_env.WebRootPath, folder, fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
               file.CopyTo(stream);
            }
            return fileName;
        } 

        public static void AddSomeServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;

                //options.User.RequireUniqueEmail = true;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
                options.Lockout.AllowedForNewUsers = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<IBasketCount, BasketCountService>();
        }
    }
}
