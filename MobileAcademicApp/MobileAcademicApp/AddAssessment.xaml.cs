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
        
        int _courseId;              // This is the course to which this assessment will belong, by default

        public AddAssessment(int courseId)
        {
            InitializeComponent();  // Initializing
            _courseId = courseId;   // Setting the course ID
        }

        private async void saveAssessmentButton_Clicked(object sender, EventArgs e)
        {
            // Updating the database with the user-provided information (and the _courseId)
            await Services.DatabaseService.AddAssessment(_courseId, assessmentName.Text, 
                assessmentType.SelectedItem.ToString(), assessmentDueDate.Date);
            await Shell.Current.GoToAsync("..");
        }
    }
}