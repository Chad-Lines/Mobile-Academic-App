using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using System.IO;

namespace Mobile_Academic_App.Droid
{
    [Activity(Label = "Mobile_Academic_App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            string fileName = "academic_db.db3";                                                                // The name of the database
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);    // The folder path where the database will be stored
            string completePath = Path.Combine(folderPath, fileName);                                           // The complete path to the database

            LoadApplication(new App(completePath));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}