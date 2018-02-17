using System;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class App : Application
    {
        public static Uri BaseAddress { get; set; } = new Uri("https://chaoprayaboat.azurewebsites.net");

        public App()
        {
            InitializeComponent();


            if(Helpers.Settings.Email =="")
            {
                MainPage = new LoginPage();  
            }
            else
            {
                var mp = new MasterDetailPage();
                mp.Master = new MenuPage();
                mp.Detail = new NavigationPage(new MainPage());
                MainPage = mp;
            }
           
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
