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
            addNewCourseButton.IsVisible = false;
        }

        private async void saveTermButton_Clicked(object sender, EventArgs e)
        {
            // Updating the database with the user-provided information
            await Services.DatabaseService.AddTerm(termName.Text, termStartDate.Date, termEndDate.Date);
            await Navigation.PushAsync(new MainPage());
            addNewCourseButton.IsVisible = true;
        }

        private void addNewCourseButton_Clicked(object sender, EventArgs e)
        {
            Models.Term _term = new Models.Term();


            // When the "Add New Course" button is clicked, we navigate to the Add Course page
            // ******************* THIS IS GOING TO CAUSE PROBLEMS *******************
            Navigation.PushAsync(new AddCourse(_term));
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