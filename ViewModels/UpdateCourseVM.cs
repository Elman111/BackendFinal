using Microsoft.AspNetCore.Http;

namespace BackFinal.ViewModels
{
    public class UpdateCourseVM
    {

        public string Title { get; set; }
        public string Desc { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Photo { get; set; }
    }
}
