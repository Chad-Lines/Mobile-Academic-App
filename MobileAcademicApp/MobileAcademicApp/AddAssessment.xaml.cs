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
    public partial class AddAssessment : ContentPage
    {
        
        Models.Course _course;      // This is the course to which this assessment will belong, by default

        public AddAssessment(Models.Course course)
        {
            InitializeComponent();  // Initializing
            _course = course;       // Setting the associated course
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            /*
             ************************************************************************************** 
             * Objective A.3: Add two assessments for each course - one objective, one performance
             * Objective B.5: Add, edit, and delete assessments
             **************************************************************************************
             */

            // Capturing the status as a string so we can evaluate it in the data validation check
            string status = assessmentType.SelectedItem.ToString();

            // Check that another assessment of the same type doesn't already exist
            if (Services.DataValidation.CheckAssessmentType(_course, status))
            {
                // Create the new model based on user input
                Models.Assessment assessment = new Models.Assessment();
                assessment.CourseId = _course.Id;
                assessment.Name = assessmentName.Text;
                assessment.DueDate = assessmentDueDate.Date;
                assessment.Type = status;
                assessment.Notify = Notify.IsChecked;

                // Adding the assessment to the database
                await Services.DatabaseService.AddAssessment(assessment);

                // Navigate back to the course detail page
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                // Display an error if an assessment of this type already exists
                await DisplayAlert("Error", "Courses can have only one assessment of each type.", "OK");
            }
            
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the course detail page
            await Navigation.PushAsync(new CourseDetail(_course));
        }
    }
}