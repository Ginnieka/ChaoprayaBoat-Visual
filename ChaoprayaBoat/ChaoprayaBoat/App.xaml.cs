using System;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class App : Application
    {
        public static Uri BaseAddress { get; set; } = new Uri("https://chaoprayaboat.azurewebsites.net");
        public static string GooglePlaceApiKey1 { get; set; } = "AIzaSyAIBf9ftcng2FURH6eqrX4tHnlOOqwFBz0";
        public static string GooglePlaceApiKey2 { get; set; } = "AIzaSyB-9_oZNotKSzH-3wFr8Fgu6L2N5NGn_g8";


        public App()
        {
            InitializeComponent();


            if (!Helpers.Settings.IsTutorial)
            {
                int max = 3;
                var cp = new CarouselPage();
                for (int i = 1; i <= max; i++)
                {
                    if (i == 1)
                    {
                        var tutor = new ContentPage();
                        var grid = new Grid();
                        var image = new Image();
                        image.Aspect = Aspect.Fill;
                        image.Source = $"step{i}.jpg";
                        grid.Children.Add(image);

                        var button = new Button();
                        button.Text = "ข้าม";
                        button.VerticalOptions = LayoutOptions.End;
                        button.Margin = new Thickness(20);
                        //button.TextColor = Color.FromRgb(0xFF, 0xFF, x0FF);
                        button.TextColor = Color.Black;
                        button.BackgroundColor = Color.Lime;
                        grid.Children.Add(button);
                        button.Clicked += (sender, e) => {
                            Helpers.Settings.IsTutorial = true;
                            MainPage = new LoginPage(); 
                        };

                        tutor.Content = grid;
                        cp.Children.Add(tutor);
                    }
                    else if (i == max)
                    {
                        var tutor = new ContentPage();
                        var grid = new Grid();
                        var image = new Image();
                        image.Aspect = Aspect.Fill;
                        image.Source = $"step{i}.jpg";
                        grid.Children.Add(image);

                        var button = new Button();
                        button.Text = "เริ่มใช้งาน";
                        button.VerticalOptions = LayoutOptions.End;
                        button.Margin = new Thickness(20);
                        button.TextColor = Color.Black;
                        button.BackgroundColor = Color.Lime;
                        grid.Children.Add(button);
                        button.Clicked += (sender, e) => {
                            Helpers.Settings.IsTutorial = true;
                            MainPage = new LoginPage();
                        };

                        tutor.Content = grid;
                        cp.Children.Add(tutor);

                    }
                    else
                    {

                        var tutor = new ContentPage();
                        var image = new Image();
                        image.Aspect = Aspect.Fill;
                        image.Source = $"step{i}.jpg";
                        tutor.Content = image;
                        cp.Children.Add(tutor);
                    }

                }



                MainPage = cp;
            }
            else if(Helpers.Settings.IsLoggedIn)
            {
                var mp = new MasterDetailPage();
                mp.Master = new MenuPage();
                mp.Detail = new NavigationPage(new MainPage());
                MainPage = mp;
            }
            else
            {
                MainPage = new LoginPage(); 
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
