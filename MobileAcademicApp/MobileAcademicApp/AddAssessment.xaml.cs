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
            // Create the new model based on user input
            Models.Assessment assessment = new Models.Assessment();
            assessment.CourseId = _course.Id;
            assessment.Name = assessmentName.Text;
            assessment.DueDate = assessmentDueDate.Date;
            assessment.Type = assessmentType.SelectedItem.ToString();

            // Adding the assessment to the database
            await Services.DatabaseService.AddAssessment(assessment);

            // Navigate back to the course detail page
            await Navigation.PushAsync(new MainPage());
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            // Navigate back to the course detail page
            await Navigation.PushAsync(new CourseDetail(_course));
        }
    }
}