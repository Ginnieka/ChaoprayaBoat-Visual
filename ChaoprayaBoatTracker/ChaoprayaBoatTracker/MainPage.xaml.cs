using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoatTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            loginButton.Clicked += LoginButton_Clicked;
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
            var response = await client.PostAsync("api/DriverAuthen", content);
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

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                await DisplayAlert("Warning", "No time table", "OK");
                return;
            }

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return;
            }

            var json = await response.Content.ReadAsStringAsync();
            var timetables = JArray.Parse(json).ToObject<List<TimeTable>>();

            await Navigation.PushModalAsync(new KeepCoordinatePage(timetables));
        }
    }
}
