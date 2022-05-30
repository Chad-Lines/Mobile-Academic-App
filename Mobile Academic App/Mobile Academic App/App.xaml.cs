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

            MainPage = new MainPage();
        }

        public App(string filePath)
        {
            // This function overrides part of the main App class
            InitializeComponent();
            MainPage = new MainPage();
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
