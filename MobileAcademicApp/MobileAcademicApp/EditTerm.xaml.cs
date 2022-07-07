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
    public partial class EditTerm : ContentPage
    {
        Models.Term _term;

        public EditTerm(Models.Term term)
        {
            InitializeComponent();
            _term = term;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            termName.Text = _term.Name;                                                 // Setting the term name label
            termStartDate.Date = _term.StartDate;                                       // Setting the start date label
            termEndDate.Date = _term.EndDate;                                           // Setting the end date label

            var courses = await Services.DatabaseService.GetCoursesForTerm(_term.Id);   // Getting a list of associated courses

            if (courses == null)                                                        // If there are no courses...
            {
                return;                                                                 // Just return
            }
            else                                                                        // Otherwise...
            {
                noCourseLabel.IsVisible = false;                                        // Hide the "no courses" label
                coursesCollectionView.ItemsSource = courses;                                   // Send the courses as the reference for the course list
            }
        }
        private async void saveTermButton_Clicked(object sender, EventArgs e)
        {
            // Creating a term object based on user input
            Models.Term updatedTerm = new Models.Term();
            updatedTerm.Name = termName.Text;
            updatedTerm.StartDate = termStartDate.Date;
            updatedTerm.EndDate = termEndDate.Date;

            // Updating the database with the user-provided information
            await Services.DatabaseService.UpdateTerm(_term.Id, updatedTerm);

            // Navigating back to the main page
            await Navigation.PushAsync(new MainPage());
        }
        private async void cancelButton_Clicked(object sender, EventArgs e)
        {
            // When the user clicks Cancel, we just navigate back to the term detail page
            await Navigation.PushAsync(new TermDetail(_term));
        }
        private async void addNewCourseButton_Clicked(object sender, EventArgs e)
        {
            /*
             ***********************************************************************************
             * Objective B.2: Provide information about the term and allow user to add courses
             *********************************************************************************** 
             */

            // Go to the New Course screen
            await Navigation.PushAsync(new AddCourse(_term));
        }

        private void coursesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}