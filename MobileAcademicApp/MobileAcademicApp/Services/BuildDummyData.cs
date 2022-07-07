using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAcademicApp.Services
{
    internal class BuildDummyData
    {
        public async static void BuildData()
        {
            // Build the term
            Models.Term term = new Models.Term();
            term.Name = "DUMMY TERM";
            term.StartDate = new DateTime(2022, 07, 01);
            term.EndDate = new DateTime(2022, 12, 31);

            // Add the term to the database
            await Services.DatabaseService.AddTerm(term);

            // Build the course
            Models.Course course = new Models.Course();
            course.Name = "DUMMY COURSE";
            course.StartDate = new DateTime(2022, 07, 01);
            course.EndDate = new DateTime(2022, 12, 31);
            course.Status = "In Progress";
            course.InstructorName = "Chad Lines";
            course.InstructorPhoneNumber = "253-850-6547";
            course.InstructorEmail = "clines@wgu.edu";
            course.Notes = "Dummy course notes";
            course.TermId = term.Id;

            // Add the course to the database
            await Services.DatabaseService.AddCourse(course);

            // Build the first assessment
            Models.Assessment assessment1 = new Models.Assessment();
            assessment1.Name = "DUMMY ASSESSMENT 1";
            assessment1.DueDate = new DateTime(2022, 12, 31);
            assessment1.Type = "Performance";
            assessment1.CourseId = course.Id;

            // Add the  to the database
            await Services.DatabaseService.AddAssessment(assessment1);

            // Build the second assessment
            Models.Assessment assessment2 = new Models.Assessment();
            assessment2.Name = "DUMMY ASSESSMENT 2";
            assessment2.DueDate = new DateTime(2022, 12, 31);
            assessment2.Type = "Objective";
            assessment2.CourseId = course.Id;

            // Add the assessment to the database
            await Services.DatabaseService.AddAssessment(assessment2);
        }
    }
}
