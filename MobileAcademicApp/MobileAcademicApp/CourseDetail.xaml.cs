using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MobileAcademicApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetail : ContentPage
    {
        Models.Course _course;

        public CourseDetail(Models.Course course)
        {
            InitializeComponent();
            _course = course;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Setting the course details
            courseName.Text = "Name: " + _course.Name;                         
            startDate.Text = "Start Date: " + _course.StartDate.ToString("MM/dd/yy");
            endDate.Text = "End Date: " + _course.EndDate.ToString("MM/dd/yy");
            status.Text = "Status: " + _course.Status.ToString();
            instructorName.Text = "Instructor Name: " + _course.InstructorName;
            instructorPhone.Text = "Instructor Phone: " + _course.InstructorPhoneNumber;
            instructorEmail.Text = "Instructor Email: " + _course.InstructorEmail;
            notify.Text = "Notifications Enabled: " + _course.Notify.ToString(); 
            notes.Text = "Notes: " + _course.Notes;

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

        private async void editCourse_Clicked(object sender, EventArgs e)
        {
            // When the "Edit" button is clicked, we navigate to EditCourse, sending the current course
            // as the parameter
            await Navigation.PushAsync(new EditCourse(_course));
        }

        private async void deleteCourse_Clicked(object sender, EventArgs e)
        {
            // Get the term that this course belongs to so that we can navigate back to the term page
            Models.Term _term = await Services.DatabaseService.GetTermByID(_course.TermId);

            await Services.DatabaseService.RemoveCourse(_course.Id);    // Remove the current Term
            await Navigation.PushAsync(new TermDetail(_term));          // Navigate back to the main page
        }

        private async void assessmentsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When the user selects an assessment to view, we transitiont to the appropriate page

            if (e.CurrentSelection == null)                                                             // If the selection is invalid...
            {
                return;                                                                                 // Simply return
            }
            else                                                                                        // Otherwise...
            {
                Models.Assessment assessment = (Models.Assessment)e.CurrentSelection.FirstOrDefault();  // Load the term
                await Navigation.PushAsync(new AssessmentDetail(assessment));                           // Navigate to the edit assessment page
            }
        }

        private async void shareNotes_Clicked(object sender, EventArgs e)
        {
            /*
             ******************************************************************
             * Objective B.4: Allow the user to share their notes
             ****************************************************************** 
             */
            await Share.RequestAsync(new ShareTextRequest { Text = notes.Text, Title="Share Note" });
        }
    }
}