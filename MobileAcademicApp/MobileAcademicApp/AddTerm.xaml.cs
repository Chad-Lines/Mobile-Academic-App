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
            /*
             ************************************************************ 
             * Objective A.1: Add Academic Terms as Needed
             ************************************************************ 
             */


            Models.Term term = new Models.Term();           // Create a new term object
            term.Name = termName.Text;                      // Sett the parameters per user input
            term.StartDate = termStartDate.Date;
            term.EndDate = termEndDate.Date;
            
            await Services.DatabaseService.AddTerm(term);   // Updating the database with the user-provided information
            await Navigation.PushAsync(new MainPage());     // Navigate back to the main page
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