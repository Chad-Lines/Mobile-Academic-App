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
        public AddCourse()
        {
            InitializeComponent();
        }

        private void saveCourseButton_Clicked(object sender, EventArgs e)
        {

        }

        private void addAssessmentButton_Clicked(object sender, EventArgs e)
        {
            // When the "Add Assessment" button is clicked, we navigate to the Add Assessment page
            Navigation.PushAsync(new AddAssessment());
        }
    }
}