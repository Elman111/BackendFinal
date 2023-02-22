using System.Collections.Generic;

namespace BackFinal.Models
{
    public class Feature
    {
        public int Id { get; set; }

        public string Starts { get; set; }

        public string Duration { get; set; }

        public string ClassDuration { get; set; }

        public string SkillLevel { get; set; }

        public string Language { get; set; }

        public int Students { get; set; }

        public string Assesments { get; set; }

        public int CourseId { get; set; }
    }
}