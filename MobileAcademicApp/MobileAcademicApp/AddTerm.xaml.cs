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
    public partial class AddTerm : ContentPage
    {
        public AddTerm()
        {
            InitializeComponent();
        }

        private void saveTermButton_Clicked(object sender, EventArgs e)
        {

        }

        private void addNewCourseButton_Clicked(object sender, EventArgs e)
        {
            // When the "Add New Course" button is clicked, we navigate to the Add Course page
            Navigation.PushAsync(new AddCourse());
        }
    }
}