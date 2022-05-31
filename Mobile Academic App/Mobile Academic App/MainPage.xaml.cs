using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile_Academic_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnAddCourse_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAddOA_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOA());
        }

        private void btnAddPA_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPA());
        }
    }
}
