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

        private void newTermButton_Clicked(object sender, EventArgs e)
        {
            // When the "New" button is clicked, we navigate to the Add Term page
            Navigation.PushAsync(new AddTerm());    
        }

        #region ComeBackTo
        private void Term1Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Term2Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Term3Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Term4Button_Clicked(object sender, EventArgs e)
        {

        }
        #endregion


    }
}
