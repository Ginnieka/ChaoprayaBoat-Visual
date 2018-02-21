using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace ChaoprayaBoat
{
    public partial class MainPage : ContentPage
    {
        Map map;
        bool isFocusSource;
        bool isFocusDest;
        //PortListPage portListPage;
        PlacePage placePage;

        Coordinate sourcePort;
        Coordinate destPort;

        public MainPage()
        {
            InitializeComponent();

            nearestButton.Clicked += NearestButton_Clicked;
            mapTypeButton.Clicked += MapTypeButton_Clicked;
            soureEntry.Focused += NaviEntry_Focused;
            destinationEntry.Focused += NaviEntry_Focused;
            closeRouteListViewButton.Clicked += CloseRouteListViewButton_Clicked;
            GetLocation();

        }

        async void GetLocation()
        {
            var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));
       

            //var stack = new StackLayout { Spacing = 0 };
            //stack.Children.Add(map);
            //Content = stack;
            map = new Map(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HasZoomEnabled = true
            };

            mapView.Content = map;
           

            var client = new HttpClient();
            client.BaseAddress = App.BaseAddress;
            var response = await client.GetAsync("api/GetPorts");

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var ports = JArray.Parse(json).ToObject<List<Coordinate>>();


                foreach (var port in ports)
                {
                    var pos = new Position(port.Latitude, port.Longtitude); // Latitude, Longitude
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = pos,
                        Label = port.Name
                        //Address = "มหาลัยศิลปากร"
                    };
                    map.Pins.Add(pin);
                }
            }

        }


        void NearestButton_Clicked(object sender, EventArgs e)
        {
            //runtime
            var nearestView = new NearestView(this);
            mainLayout.Children.Add(nearestView);
        }

        public void SetSourceNavi(Coordinate port)
        {
            soureEntry.Text = port.Name;
            sourcePort = port;

            GetNavi();
        }

        async void MapTypeButton_Clicked(object sender, EventArgs e)
        {
            var result = await DisplayActionSheet("แผนที่", "ยกเลิก", null, "Hybrid", "Satelite", "Street");

            if (result == "Hybrid") map.MapType = MapType.Hybrid;
            else if (result == "Satelite") map.MapType = MapType.Satellite;
            else if (result == "Street") map.MapType = MapType.Street; 
        }

        void NaviEntry_Focused(object sender, FocusEventArgs e)
        {
            if (sender == soureEntry) isFocusSource = true;
            else if (sender == destinationEntry) isFocusDest = true;

            routeListView.IsVisible = false;
            closeRouteListViewButton.IsVisible = false;

            //portListPage = new PortListPage(true);
            //Navigation.PushAsync(portListPage);
            placePage = new PlacePage();
            Navigation.PushAsync(placePage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (isFocusSource) 
            {
                if (placePage.SelectedPort != null)
                {
                    soureEntry.Text = placePage.SelectedPort.Name;
                    sourcePort = placePage.SelectedPort;
                }
                else
                {
                    soureEntry.Text = "";
                    sourcePort = null;
                }
            }
            else if (isFocusDest) 
            {
                if (placePage.SelectedPort != null)
                {
                    destinationEntry.Text = placePage.SelectedPort.Name;
                    destPort = placePage.SelectedPort;
                }
                else
                {
                    destinationEntry.Text = "";
                    destPort = null;
                }
            }

            isFocusSource = false;
            isFocusDest = false;

            GetNavi();
        }

        public async void GetNavi()
        {
            if (sourcePort != null && destPort != null)
            {
                var client = new System.Net.Http.HttpClient();
                client.BaseAddress = App.BaseAddress;

                var response = await client.GetAsync($"api/GetNavi?sourceCoordinateId={sourcePort.Id}&destinationCoordinteId={destPort.Id}");

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var routes = JArray.Parse(json).ToObject<List<Route>>();

                    if (routes.Count > 0)
                    {
                        routeListView.ItemsSource = routes;
                        routeListView.IsVisible = true;
                        closeRouteListViewButton.IsVisible = true;
                    }
                    else await DisplayAlert("", "No route found", "OK");
                }
            }
        }

        void CloseRouteListViewButton_Clicked(object sender, EventArgs e)
        {
            routeListView.IsVisible = false;
            closeRouteListViewButton.IsVisible = false;

            sourcePort = null;
            destPort = null;

            soureEntry.Text = "";
            destinationEntry.Text = "";
        }
    }  


}
