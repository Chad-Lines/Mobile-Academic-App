using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAcademicApp.Services
{
    internal class DataValidation
    {
        public static bool CheckForNull(string s)
        {
            // Check if the provided entry is null. If it is, we return the check as false (i.e. failed)
            // Otherwise we return true (i.e. passed)
            if (String.IsNullOrEmpty(s)) { return false; }
            else { return true; }
        }

        public static bool CheckEmail(string email)
        {
            
            if (String.IsNullOrEmpty (email)) { return false; } // If the email is empty, the check fails

            var trimmedEmail = email.Trim();                    // Trim whitespace 
            if (trimmedEmail.EndsWith(".")) { return false; }   // If the email ends in "." it fails the test

            try
            {
                // Shortcutting by using the MailAddress class to verify the email address format.
                // We then return bool whether the trimmed email equals the verified email.
                // If it does, then the address is valid. If not, then it's invalid.
                var addr = new System.Net.Mail.MailAddress(email);  
                return addr.Address == trimmedEmail;
            }
            catch
            {
                // If the above check fails (i.e. the email is unvalidated by the MailAddress class),
                // then it's invalid and we return false.
                return false;
            }
            
        }

        public static bool CheckDates(DateTime first, DateTime last)
        {
            // If the first date is before the last date, return true
            // Otherwise return false
            return first < last ? true : false;
        }

        public static bool CheckCourseCount(Models.Term term)
        {
            
            int count = 0;                                                      // Set count variable 
            var courses = Services.DatabaseService.GetCoursesForTerm(term.Id);  // Get the courses for the term provided 

            foreach (Models.Course c in courses.Result) count++;                // Count the courses in the term
            if (count >= 6) return false;                                       // If there are 6 or more, then check fails
            else return true;                                                   // Otherwise, the check succeeds
        }

        public static bool CheckAssessmentType(Models.Course course, string assessmentType)
        {
            var assessments = Services.DatabaseService.GetAssessmentsForCourse(course.Id);  // Get the assessments for the term provided

            if (assessments == null) { return true; }                                       // If there are no assessments, return true
            else
            {
                foreach (Models.Assessment a in assessments.Result)                         // Comparing the assessments
                {
                    if (a.Type == assessmentType) { return false; }                         // If an assessment of the same type exists, fail
                }
            }           

            return true;                                                                    // Otherwise return true
        }
    }
}