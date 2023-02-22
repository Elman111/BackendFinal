using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using BackFinal.ViewModels;

namespace BackFinal.Models
{
    public class Teachers
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }


        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }

        public List<AboutVM>About { get; set; }

        public string Facebook { get; set; }

        public string LinkedIn { get; set; }

        public string Instagram { get; set; }

        public string Whatsapp { get; set; }

        public int Number { get; set; }

        public string Email { get; set; }

        public string Degree { get; set; }

        public string Experience { get; set; }

        public string Hobby { get; set; }

        public string Faculty { get; set; }

    }
}
