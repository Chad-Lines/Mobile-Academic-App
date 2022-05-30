using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Academic_App
{
    public partial class App : Application
    {

        public static string FilePath;

        public App()
        {
            InitializeComponent();            
        }

        public App(string filePath)
        {
            // This function overrides part of the main App class
            InitializeComponent();

            // This sets the main page to TermsPage and sets it as the root for 
            // navigation.
            MainPage = new NavigationPage(new TermsPage());
            FilePath = filePath;

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
