using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileAcademicApp.Models
{
    public class Course
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhoneNumber { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
        public List<Assessment> Assessments { get; set; }
    }
}
