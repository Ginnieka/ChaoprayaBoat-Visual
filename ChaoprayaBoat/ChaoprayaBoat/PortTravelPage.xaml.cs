using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class PortTravelPage : ContentPage
    {
        Coordinate port;

        public PortTravelPage()
        {
            InitializeComponent();
            portTravelListView.ItemTapped += PortTravelListView_ItemTapped;
        }

        public PortTravelPage(Coordinate port) : this()
        {
            this.port = port;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetData();
        }

        async void GetData()
        {
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = App.BaseAddress;

            var response = await client.GetAsync($"api/GetTravel?coordinateId={port.Id}");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var travels = JArray.Parse(json).ToObject<List<Travel>>();
                portTravelListView.ItemsSource = travels;
            }
         
            
        }

        void PortTravelListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var travel = e.Item as Travel;

            Navigation.PushModalAsync(new PortTravelDetail(travel));
        }
    }

}
