using BackFinal.Models;
using System.Collections.Generic;

namespace BackFinal.ViewModels
{
    public class CourseVM
    {
        //public Slider Sliders { get; set; }

        public List<Course> Course { get; set; }
        public List<Course> Courses { get; internal set; }
    }

    
}
