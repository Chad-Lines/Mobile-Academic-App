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
            _term = term;
        }

        private void saveCourseButton_Clicked(object sender, EventArgs e)
        {

        }

        private void addAssessmentButton_Clicked(object sender, EventArgs e)
        {
            // When the "Add Assessment" button is clicked, we navigate to the Add Assessment page
            // TODO: PASS THE COURSE ID
            Navigation.PushAsync(new AddAssessment(0));
        }
    }
}