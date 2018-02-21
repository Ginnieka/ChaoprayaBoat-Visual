using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Geolocator;
using Newtonsoft.Json.Linq;
using ChaoprayaBoat.Library.Models;
using Plugin.Geolocator.Abstractions;

namespace ChaoprayaBoat
{
    public partial class NearestView : ContentView
    {
        Position position;
        public PlacePage placePage { get; set; }
        public MainPage mainPage { get; set; }

        public NearestView()
        {
            InitializeComponent();

            closeButton.Clicked += CloseButton_Clicked;
            portListView.Refreshing += PortListView_Refreshing;             
            portListView.ItemTapped += PortListView_ItemTapped;
            GetData();
        }

        public NearestView(MainPage page) : this()
        {
            this.mainPage = page;
        }


        public NearestView(Position position, PlacePage page)
        {
            InitializeComponent();
            this.placePage = page;

            closeButton.Clicked += CloseButton_Clicked;
            portListView.Refreshing += PortListView_Refreshing;
            portListView.ItemTapped += PortListView_ItemTapped;

            this.position = position;

            GetData();
        }

        async void GetData()
        {
            if (position == null) position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));
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
            if (placePage != null)
            {
                placePage.SelectedPort = e.Item as Coordinate;
                (placePage.Content as Grid).Children.Remove(this);
                placePage.Navigation.PopAsync();
            }
            else
            {
                mainPage.SetSourceNavi(e.Item as Coordinate);
                (mainPage.Content as Grid).Children.Remove(this);
            }


            //var tp = new TabbedPage();
            //tp.Children.Add(new PortPage(port));
            //tp.Children.Add(new BoatGoPage(port));
            //tp.Children.Add(new BoatBackPage(port));
            //tp.Children.Add(new PortTravelPage());
            //tp.Title = port.Name;

            //Navigation.PushAsync(tp);
        }
    }
}
