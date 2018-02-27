using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class LoginPage : ContentPage
    {
        public IFacebookManager manager;

        public LoginPage()
        {
            InitializeComponent();
            loginButton.Clicked += LoginButton_Clicked;
            signupButton.Clicked += SignupButton_Clicked;
            fbButton.ShowingLoggedInUser += FbButton_ShowingLoggedInUser;
            fbButton.ShowingLoggedOutUser += FbButton_ShowingLoggedOutUser;

            manager = DependencyService.Get<IFacebookManager>();
            if (manager == null) throw new NotImplementedException("No IFacebookManager implementation found");
        }

        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var param = new Dictionary<string, string>();
            param.Add("username", usernameEntry.Text);
            param.Add("password", passwordEntry.Text);

            var content = new System.Net.Http.FormUrlEncodedContent(param);

            var client = new HttpClient();
            client.BaseAddress = App.BaseAddress;

            loginButton.IsEnabled = false;
            usernameEntry.IsEnabled = false;
            passwordEntry.IsEnabled = false;
            indicator.IsVisible = true;
            var response = await client.PostAsync("api/MemberAuthen", content);
            loginButton.IsEnabled = true;
            usernameEntry.IsEnabled = true;
            passwordEntry.IsEnabled = true;
            indicator.IsVisible = false;

            //
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                await DisplayAlert("Warning", "User or Password incorrect", "OK");
                return;
            }



            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            var member = JObject.Parse(json).ToObject<Member>();

            Helpers.Settings.IsLoggedIn = true;
            Helpers.Settings.MemberId = member.Id;
            Helpers.Settings.Fullname = member.Fullname;
            Helpers.Settings.Picture = member.FacebookPicture;

            //change page no bck to login  -- mainpage
            var app = Parent as App;

            var mp = new MasterDetailPage();
            mp.Master = new MenuPage();
            mp.Detail = new NavigationPage(new MainPage());

            app.MainPage = mp;

        }
        void SignupButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SignupPage());
        }

        async void FbButton_ShowingLoggedInUser(object sender, ChaoprayaBoat.FacebookLoginEventArgs e)
        {
            indicatorFacebook.IsVisible = true;
            usernameEntry.IsEnabled = false;
            passwordEntry.IsEnabled = false;
            loginButton.IsEnabled = false;
            signupButton.IsEnabled = false;
            fbButton.IsEnabled = false;

            var param = new Dictionary<string, string>();
            param.Add("name", e.User.FirstName);
            param.Add("lastName", e.User.LastName);
            param.Add("facebookId", e.User.Id);
            param.Add("facebookToken" ,e.User.Token);
            param.Add("facebookPicture", e.User.Picture);
            param.Add("email", "");
            param.Add("password","");

            var content = new System.Net.Http.FormUrlEncodedContent(param);
            var client = new HttpClient();
            client.BaseAddress = App.BaseAddress;

            var response = await client.PostAsync("api/SignUp", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var memberId = int.Parse(json);

                if (memberId != 0)
                {
                    Helpers.Settings.IsLoggedIn = true;
                    Helpers.Settings.MemberId = memberId;
                    Helpers.Settings.Fullname = $"{e.User.FirstName} {e.User.LastName}";
                    Helpers.Settings.Picture = e.User.Picture;

                    var app = Parent as App;

                    var mp = new MasterDetailPage();
                    mp.Master = new MenuPage();
                    mp.Detail = new NavigationPage(new MainPage());

                    app.MainPage = mp;
                }
                else
                {
                    indicatorFacebook.IsVisible = false;
                    usernameEntry.IsEnabled = true;
                    passwordEntry.IsEnabled = true;
                    loginButton.IsEnabled = true;
                    signupButton.IsEnabled = true;
                    fbButton.IsEnabled = true;
                    await DisplayAlert("", $"โปรดตรวจสอบ อินเตอร์เน็ต แล้วลองอีกครั้ง", "ตกลง");
                }

            }
            else
            {
                indicatorFacebook.IsVisible = false;
                usernameEntry.IsEnabled = true;
                passwordEntry.IsEnabled = true;
                loginButton.IsEnabled = true;
                signupButton.IsEnabled = true;
                fbButton.IsEnabled = true;
            }
        }

        void FbButton_ShowingLoggedOutUser(object sender, EventArgs e)
        {
            
        }

    }
}
