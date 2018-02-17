using System;
using Xamarin.Forms;

namespace ChaoprayaBoatTracker
{
    public partial class App : Application
    {
        public static Uri BaseAddress { get; set; } = new Uri("https://chaoprayaboat.azurewebsites.net");
        
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
