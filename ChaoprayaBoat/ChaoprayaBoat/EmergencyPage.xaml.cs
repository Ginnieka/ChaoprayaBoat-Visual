using System;
using System.Collections.Generic;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class EmergencyPage : ContentPage
    {
        public EmergencyPage()
        {
            InitializeComponent();
            emergencyListView.ItemTapped += EmergencyListView_ItemTapped;

            GetData();
        }

        async void GetData()
        {
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = App.BaseAddress;

            var response = await client.GetAsync("api/GetEmergencies");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var emergencies = JArray.Parse(json).ToObject<List<Emergency>>();

                emergencyListView.ItemsSource = emergencies;
            }

        }

        async void EmergencyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var emergency = e.Item as Emergency;

            var isOK = await DisplayAlert(emergency.Name, emergency.Telephone, "โทร", "ยกเลิก");
            if (isOK)
            {
                Device.OpenUri(new Uri($"tel:{emergency.Telephone}"));
            }





        }
    }
}
