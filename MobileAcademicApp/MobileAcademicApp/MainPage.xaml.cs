using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileAcademicApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // When the page loads, update the list of terms
            termsCollectionView.ItemsSource = await Services.DatabaseService.GetAllTerms();
        }

        private async void newTermButton_Clicked(object sender, EventArgs e)
        {
            // When the "New" button is clicked, we navigate to the Add Term page
            await Navigation.PushAsync(new AddTerm());    
        }

        private async void termsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When the user selects a term to view, we transitiont to the appropriate page

            if (e.CurrentSelection == null)                                             // If the selection is invalid...
            {
                return;                                                                 // Simply return
            }
            else                                                                        // Otherwise...
            {
                Models.Term term = (Models.Term)e.CurrentSelection.FirstOrDefault();    // Load the term
                await Navigation.PushAsync(new TermDetail(term));                       // Navigate to the edit term page
            }
        }
    }
}
