using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackFinal.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Desc { get; set; }


        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }

        public List<Feature> Feature { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
