using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Geolocator;
using Newtonsoft.Json.Linq;
using ChaoprayaBoat.Library.Models;

namespace ChaoprayaBoat
{
    public partial class NearestView : ContentView
    {
        public NearestView()
        {
            InitializeComponent();

            closeButton.Clicked += CloseButton_Clicked;
            portListView.Refreshing += PortListView_Refreshing;
            GetData();

            portListView.ItemTapped += PortListView_ItemTapped;
        }

        async void GetData()
        {
            var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));
            var param = new Dictionary<string, string>();
            param.Add("latitude", position.Latitude.ToString());
            param.Add("longitude",position.Longitude.ToString());

            var content = new System.Net.Http.FormUrlEncodedContent(param);
            var client = new System.Net.Http.HttpClient();
            client.BaseAddress = App.BaseAddress;

            portListView.IsRefreshing = true;

            var response = await client.PostAsync("api/GetPortNearest", content);

            portListView.IsRefreshing = false;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var coordinates = JArray.Parse(json).ToObject<List<Coordinate>>();
                foreach (var item in coordinates)
                {
                    item.GetDistacneInfo(position.Latitude, position.Longitude);
                }
                portListView.ItemsSource = coordinates;
            }
        }

        void CloseButton_Clicked(object sender, EventArgs e)
        {
            var mainLayout = Parent as Grid;
            mainLayout.Children.Remove(this);
        }

        void PortListView_Refreshing(object sender, EventArgs e)
        {
            GetData();
        }

        void PortListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var port = e.Item as Coordinate;
            var tp = new TabbedPage();
            tp.Children.Add(new PortPage(port));
            tp.Children.Add(new BoatGoPage(port));
            tp.Children.Add(new BoatBackPage(port));
            tp.Children.Add(new PortTravelPage());
            tp.Title = port.Name;

            Navigation.PushAsync(tp);
        }
    }
}
