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

        private async void saveTermButton_Clicked(object sender, EventArgs e)
        {
            // Updating the database with the user-provided information
            await Services.DatabaseService.AddTerm(termName.Text, termStartDate.Date, termEndDate.Date);
            await Navigation.PushAsync(new MainPage());
        }

        private void addNewCourseButton_Clicked(object sender, EventArgs e)
        {
            // When the "Add New Course" button is clicked, we navigate to the Add Course page
            Navigation.PushAsync(new AddCourse());
        }

        #region TOOLBAR ITEMS
        private void saveTermToolbar_Clicked(object sender, EventArgs e)
        {
            // Just pass through to the save button
            saveTermButton_Clicked((object)sender, new EventArgs());
        }

        private async void cancelTermToolbar_Clicked(object sender, EventArgs e)
        {
            // Return to the previous page
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}