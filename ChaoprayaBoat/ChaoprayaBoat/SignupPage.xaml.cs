using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
            signupButton.Clicked += SignupButton_Clicked;
            backButton.Clicked += BackButton_Clicked;
        }

        async void SignupButton_Clicked(object sender, EventArgs e)
        {
            var param = new Dictionary<string, string>();
            param.Add("name",nameEntry.Text);
            param.Add("lastName",lastnameEntry.Text);
            param.Add("email",emailEntry.Text);
            param.Add("password",passwordEntry.Text);

            var content = new System.Net.Http.FormUrlEncodedContent(param);
            var client = new HttpClient();
            client.BaseAddress = App.BaseAddress;

            var response = await client.PostAsync("api/SignUp", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var isOk = bool.Parse(json);

                if (isOk)
                {
                    Helpers.Settings.IsLoggedIn = true;
                    Helpers.Settings.Fullname = $"{nameEntry.Text} {lastnameEntry.Text}";
                    Helpers.Settings.Picture = "";

                    var app = Parent as App;

                    var mp = new MasterDetailPage();
                    mp.Master = new MenuPage();
                    mp.Detail = new NavigationPage(new MainPage());

                    app.MainPage = mp;
                }
                else await DisplayAlert("", $"{emailEntry.Text} เคยสมัครสมาชิกแล้ว", "ตกลง");

            }


        }

        void BackButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
