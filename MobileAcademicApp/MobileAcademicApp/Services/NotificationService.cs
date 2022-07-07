using System;
using System.Collections.Generic;
using System.Text;
using Plugin.LocalNotifications;

namespace MobileAcademicApp.Services
{
    internal class NotificationService
    {
        public static void CallCourseNotifications(IEnumerable<Models.Course> courses)
        {
            /*
             ******************************************************************** 
             * Objective B.4: Set alerts for start and end date of the course
             ********************************************************************
             */

            // For each course in the list we check the start and end dates against the current
            // date and show the appropriate notification if it's a match
            foreach (Models.Course c in courses)
            {
                // If the course starts today, show a notification
                if (c.StartDate == DateTime.Today)
                {
                    // Here we create the notification. For future reference:
                    // "Course Start": this is the title of the notification
                    // "${c.Name} starts today.": This is the body of the notification,
                    //      where "${c.Name}" is the name of the course
                    // "c.Id": This is the UID of the notification. In this case I'm just setting
                    //      it to match the course ID
                    CrossLocalNotifications.Current.Show("Course Start Notification", $"{c.Name} starts today!", c.Id);
                }
                // If the course ends today, show a notification
                else if (c.EndDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Course End Notification", $"{c.Name} ends today!", c.Id);
                }
                // If there's no match, then we just continue
                else
                {
                    continue;
                }
            }
        }

        public static void CallAssessmentNotifications(IEnumerable<Models.Assessment> assessments)
        {
            /*
             ************************************************************************* 
             * Objective B.5: Set notifications for anticipated assessment due dates
             *************************************************************************
             */

            // For each assessment, we check if the due date is today. If it is we show a notification.
            // Otherwise, we carry on
            foreach (Models.Assessment a in assessments)
            {
                if (a.DueDate == DateTime.Today)
                {
                    CrossLocalNotifications.Current.Show("Assessment Due Today!", $"Assessment: {a.Name} is due today!");
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
