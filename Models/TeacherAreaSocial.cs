using System;
namespace BackFinal.Models
{
    public class TeacherAreaSocial
    {
        public int Id { get; set; }

        public string Facebook { get; set; }

        public string Penterest { get; set; }

        public string Viemo { get; set; }

        public string Twitter { get; set; }

        public int TeacherAreaId { get; set; }

        public TeacherArea TeacherArea { get; set; }
    }
}

