using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackFinal.ViewModels
{
    public class UpdateBlogVM
    {
        public string Desc { get; set; }
        public string ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
