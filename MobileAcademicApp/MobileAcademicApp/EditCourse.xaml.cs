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
    public partial class EditCourse : ContentPage
    {
        Models.Course _course;

        public EditCourse(Models.Course course)
        {
            _course = course;       // Capturing the course
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            /*
             ********************************************************************
             * Objective B.3: Allow the user to edit details for each course
             ******************************************************************** 
             */

            base.OnAppearing();

            // Setting the course details
            courseName.Text = _course.Name;
            startDate.Date = _course.StartDate;
            endDate.Date = _course.EndDate;
            status.SelectedItem = _course.Status;
            instructorName.Text = _course.InstructorName;
            instructorPhone.Text = _course.InstructorPhoneNumber;
            instructorEmail.Text = _course.InstructorEmail;
            notes.Text = _course.Notes;

            var assessments = await Services.DatabaseService.GetAssessmentsForCourse(_course.Id);   // Getting a list of associated courses

            if (assessments == null)                                                                // If there are no courses...
            {
                return;                                                                             // Just return
            }
            else                                                                                    // Otherwise...
            {
                assessmentsCollectionView.ItemsSource = assessments;                                // Send the courses as the reference for the course list
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (Services.DataValidation.CheckForNull(courseName.Text) &&                    // Check the course name
                Services.DataValidation.CheckForNull(instructorName.Text) &&                // Check the instructor name
                Services.DataValidation.CheckForNull(instructorPhone.Text))                 // Check the instructor phone
            {
                if (Services.DataValidation.CheckEmail(instructorEmail.Text))               // Check the instructor email
                {
                    if (Services.DataValidation.CheckDates(startDate.Date, endDate.Date))   // Check the dates
                    {
                        // Create the course based on the user input
                        Models.Course updatedCourse = new Models.Course();
                        updatedCourse.Id = _course.Id;
                        updatedCourse.TermId = _course.TermId;
                        updatedCourse.Name = courseName.Text;
                        updatedCourse.StartDate = startDate.Date;
                        updatedCourse.EndDate = endDate.Date;
                        updatedCourse.Status = status.SelectedItem.ToString();
                        updatedCourse.InstructorName = instructorName.Text;
                        updatedCourse.InstructorPhoneNumber = instructorPhone.Text;
                        updatedCourse.InstructorEmail = instructorEmail.Text;

                        // Updating the database with the user-provided information
                        await Services.DatabaseService.UpdateCourse(_course.Id, updatedCourse);

                        // Navigate back to the course detail page
                        await Navigation.PushAsync(new MainPage());
                    }
                    // Display descriptive errors when the checks fail
                    else
                    {
                        await DisplayAlert("Error", "Check that the start date is before the end date.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Check that the email address is formatted correctly.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Please complete all fields.", "OK");
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the course detail page
            await Navigation.PushAsync(new CourseDetail(_course));
        }

        private async void assessmentsCollectionView_SelectionChanged(object sender, EventArgs e)
        {

        }

        private async void addNewAssessmentButton_Clicked(object sender, EventArgs e)
        {
            /*
             ********************************************************************
             * Objective B.4: Allow the user to add assessments to each course
             ********************************************************************  
             */

            // Navigate to the add assessment page
            await Navigation.PushAsync(new AddAssessment(_course));
        }
    }
}