using System;
using System.Collections.Generic;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class BoatGoPage : ContentPage
    {
        Coordinate port;
        public BoatGoPage()
        {
            InitializeComponent();
        }

        public BoatGoPage(Coordinate port) : this()
        {
            this.port = port;

            GetData();
        }

        async void GetData()
        {
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = App.BaseAddress;

            var response = await client.GetAsync($"api/GetBoatGo?coordinateid={port.Id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var boats = JArray.Parse(json).ToObject<List<BoatHistory>>();
                foreach (var item in boats)
                {
                    item.Coordinate.GetDistacneInfo(port.Latitude, port.Longtitude);
                }
                boatListView.ItemsSource = boats;
            }
        }
    }
}
