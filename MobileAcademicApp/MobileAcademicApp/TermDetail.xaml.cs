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
    public partial class TermDetail : ContentPage
    {
        Models.Term _term;

        public TermDetail(Models.Term term)
        {
            InitializeComponent();
            _term = term;           // Getting the term
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            termName.Text = _term.Name;                                                 // Setting the term name label
            termStartDate.Text = _term.StartDate.ToString("MM/dd/yy");                  // Setting the start date label
            termEndDate.Text = _term.EndDate.ToString("MM/dd/yy");                      // Setting the end date label

            var courses = await Services.DatabaseService.GetCoursesForTerm(_term.Id);   // Getting a list of associated courses

            if (courses == null)                                                        // If there are no courses...
            {
                return;                                                                 // Just return
            }
            else                                                                        // Otherwise...
            {
                noCourseLabel.IsVisible = false;                                        // Hide the "no courses" label
                courseListView.ItemsSource = courses;                                   // Send the courses as the reference for the course list
            }
            
        }

        private async void editTermButton_Clicked(object sender, EventArgs e)
        {
            // When the "Edit" button is clicked, we navigate to EditTerm, sending the current term
            // as the parameter
            await Navigation.PushAsync(new EditTerm(_term));
        }

        private async void deleteTermButton_Clicked(object sender, EventArgs e)
        {
            await Services.DatabaseService.RemoveTerm(_term.Id);    // Remove the current Term
            await Navigation.PushAsync(new MainPage());             // Navigate back to the main page
        }
    }
}