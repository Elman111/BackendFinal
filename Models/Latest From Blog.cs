using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackFinal.Models
{
    public class Latest_From_Blog
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public string Desc { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
