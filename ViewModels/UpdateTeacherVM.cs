using Microsoft.AspNetCore.Http;

namespace BackFinal.ViewModels
{
    public class UpdateTeacherVM
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }

        public string LinkedIn { get; set; }

        public string Whatsapp { get; set; }


        public string Degree { get; set; }

        public string Email { get; set; }
        public string Experience { get; set; }
        public string Faculty { get; set; }
        public string Hobby { get; set; }

        public string Number { get; set; }

        public IFormFile Photo { get; set; }
    }
}
