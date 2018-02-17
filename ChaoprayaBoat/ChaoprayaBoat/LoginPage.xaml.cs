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
        SignupPage signupPage;

        public LoginPage()
        {
            InitializeComponent();
            loginButton.Clicked += LoginButton_Clicked;
            //signupButton.Clicked += SignupButton_Clicked;
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

            Helpers.Settings.Email = member.Email;
            Helpers.Settings.Fullname = member.Fullname;

            //change page no bck to login  -- mainpage
            var app = Parent as App;

            var mp = new MasterDetailPage();
            mp.Master = new MenuPage();
            mp.Detail = new NavigationPage(new MainPage());

            app.MainPage = mp;

        }



    }
}
