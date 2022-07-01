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
        public AddAssessment()
        {
            InitializeComponent();
        }

        private void saveAssessmentButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}