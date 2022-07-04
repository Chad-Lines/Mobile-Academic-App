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

            //coursesCollectionView.ItemsSource = await Services.DatabaseService.GetCoursesForTerm(_term.Id);

            var courses = await Services.DatabaseService.GetCoursesForTerm(_term.Id);   // Getting a list of associated courses

            if (courses == null)                                                        // If there are no courses...
            {
                return;                                                                 // Just return
            }
            else                                                                        // Otherwise...
            {
                coursesCollectionView.ItemsSource = courses;                            // Send the courses as the reference for the course list
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

        private async void coursesCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When the user selects a term to view, we transitiont to the appropriate page

            if (e.CurrentSelection == null)                                                 // If the selection is invalid...
            {
                return;                                                                     // Simply return
            }
            else                                                                            // Otherwise...
            {
                Models.Course course = (Models.Course)e.CurrentSelection.FirstOrDefault();  // Load the term
                await Navigation.PushAsync(new CourseDetail(course));                       // Navigate to the edit term page
            }
        }
    }
}