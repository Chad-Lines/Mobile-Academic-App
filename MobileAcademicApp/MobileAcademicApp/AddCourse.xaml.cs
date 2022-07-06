using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAcademicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCourse : ContentPage
    {
        Models.Term _term;

        public AddCourse(Models.Term term)
        {
            InitializeComponent();

            // Capturing the term
            _term = term;
        }

        private async void saveCourseButton_Clicked(object sender, EventArgs e)
        {
            /* 
             *******************************************************************************
             * Objective A.2. Add six courses for each  term...
             *******************************************************************************
             */

            if (Services.DataValidation.CheckForNull(courseName.Text) &&                    // Check the course name
                Services.DataValidation.CheckForNull(instructorName.Text) &&                // Check the instructor name
                Services.DataValidation.CheckForNull(instructorPhone.Text))                 // Check the instructor phone
            {
                if (Services.DataValidation.CheckEmail(instructorEmail.Text))               // Check the instructor email
                {
                    if (Services.DataValidation.CheckDates(startDate.Date, endDate.Date))   // Check the dates
                    {
                        if (Services.DataValidation.CheckCourseCount(_term))                // Check the number of courses
                        {
                            // Create a new course object based on user input
                            Models.Course course = new Models.Course();
                            course.TermId = _term.Id;
                            course.Name = courseName.Text;
                            course.StartDate = startDate.Date;
                            course.EndDate = endDate.Date;
                            course.Status = status.SelectedItem.ToString();
                            course.InstructorName = instructorName.Text;
                            course.InstructorPhoneNumber = instructorPhone.Text;
                            course.InstructorEmail = instructorEmail.Text;
                            course.Notes = notes.Text;

                            // Add the course to the database
                            await Services.DatabaseService.AddCourse(course);

                            // Navigate back to the term detail page
                            await Navigation.PushAsync(new TermDetail(_term));
                        }
                        // Display descriptive errors when the checks fail
                        else
                        {
                            await DisplayAlert("Error", "Terms are limited to 6 courses.", "OK");
                        }                        
                    }                    
                    else
                    {
                        await DisplayAlert("Error", "Check that the start date is before the end date.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Check that the email address if formatted correctly.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please complete all fields.", "OK");
            }
        }
    }
}