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
    public partial class EditAssessment : ContentPage
    {
        Models.Assessment _assessment;

        public EditAssessment(Models.Assessment assessment)
        {
            InitializeComponent();
            _assessment = assessment;
        }
        protected override async void OnAppearing()
        {
            /*
             ************************************************************************************** 
             * Objective B.5: Add, edit, and delete assessments
             **************************************************************************************
             */

            base.OnAppearing();

            // Setting the course details
            assessmentName.Text = _assessment.Name;
            assessmentDueDate.Date = _assessment.DueDate;
            assessmentType.SelectedItem = _assessment.Type;
            Notify.IsChecked = _assessment.Notify;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            // Create the assessment based on the user input
            Models.Assessment updatedAssessment = new Models.Assessment();
            
            // These are inherited and do not change
            updatedAssessment.Id = _assessment.Id;
            updatedAssessment.CourseId = _assessment.CourseId;

            // These fields can be changed
            updatedAssessment.Name = assessmentName.Text;
            updatedAssessment.DueDate = assessmentDueDate.Date;
            updatedAssessment.Type = assessmentType.SelectedIndex.ToString();
            updatedAssessment.Notify = Notify.IsChecked;

            // Updating the database with the user-provided information
            await Services.DatabaseService.UpdateAssessment(_assessment.Id, updatedAssessment);

            // Navigate back to the course detail page
            await Navigation.PushAsync(new MainPage());
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the assessment detail
            await Navigation.PushAsync(new AssessmentDetail(_assessment));
        }
    }
}