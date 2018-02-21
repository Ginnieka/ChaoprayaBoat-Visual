using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Xamarin.Forms;
//using Xamarin.Forms.Maps;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using ChaoprayaBoat.Library.Google;
using Plugin.Geolocator.Abstractions;
using ChaoprayaBoat.Library.Models;

namespace ChaoprayaBoat
{
    public partial class PlacePage : ContentPage
    {
        public Coordinate SelectedPort { get; set; }

        public PlacePage()
        {
            InitializeComponent();

            //searchButton.Clicked += SearchButton_Clicked;
            placeListView.Refreshing += PlaceListView_Refreshing;
            placeListView.ItemTapped += PlaceListView_ItemTapped;
            searchEntry.TextChanged += SearchEntry_TextChanged;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            searchEntry.Focus();
        }

        async void Search()
        {
            placeListView.IsRefreshing = true;
            var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));

            //https://maps.googleapis.com/maps/api/place/textsearch/xml?query=the+mall&key=AIzaSyAIBf9ftcng2FURH6eqrX4tHnlOOqwFBz0&location=13.842224,100.49118
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com");

            var response = await client.GetAsync($"maps/api/place/textsearch/json?query={searchEntry.Text}&key={App.GooglePlaceApiKey}&location={position.Latitude},{position.Longitude}&language=th");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var place = JObject.Parse(json).ToObject<Place>();
                placeListView.ItemsSource = place.results;
            }

            placeListView.IsRefreshing = false;


        }

        //void SearchButton_Clicked(object sender, EventArgs e)
        //{
        //    Search();
        //}

        void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length > 3)
            {
                Search();
            }
        }

        void PlaceListView_Refreshing(object sender, EventArgs e)
        {
            Search();
        }

        void PlaceListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            searchEntry.Unfocus();
            
            var place = e.Item as Result;
            var position = new Position(place.geometry.location.lat, place.geometry.location.lng);

            var nearestView = new NearestView(position, this);
            nearestView.SetValue(Grid.RowSpanProperty, 2);
            mainLayout.Children.Add(nearestView);

            placeListView.SelectedItem = null;
        }
    }
}
