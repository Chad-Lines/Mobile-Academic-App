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
            /*
             ****************************************************************** 
             * Objective B.1: Create an interface for viewing academic terms
             ******************************************************************
             */


            base.OnAppearing();
            
            var terms = await Services.DatabaseService.GetAllTerms();   // Get a list of terms from the database

            if (terms.Count() == 0) {                
                Services.BuildDummyData.BuildData();                    // If the list of terms is empty, create dummy data                
                await Navigation.PushAsync(new MainPage());             // Refresh the page
            }    
            else 
            {                   
                termsCollectionView.ItemsSource = terms;                // Otherwise, set the item source to the terms
            }                 
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
