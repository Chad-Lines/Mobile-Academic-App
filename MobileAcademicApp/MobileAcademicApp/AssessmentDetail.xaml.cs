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
    public partial class AssessmentDetail : ContentPage
    {
        Models.Assessment _assessment;

        public AssessmentDetail(Models.Assessment assessment)
        {
            InitializeComponent();
            _assessment = assessment;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Setting the assessment details
            assessmentName.Text = "Name: " + _assessment.Name;
            assessmentDueDate.Text = "Due Date: " + _assessment.DueDate.ToString("MM/dd/yy");
            assessmentType.Text = "Type: " + _assessment.Type;
            assessmentNotify.Text = "Notifications Enabled: " + _assessment.Notify.ToString();
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            // Navigate to the Edit Assessment page
            await Navigation.PushAsync(new EditAssessment(_assessment));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            // Delete the assessment
            await Services.DatabaseService.RemoveAssessment(_assessment.Id);

            // Navigate back to the main page
            await Navigation.PushAsync(new MainPage());
        }
    }
}