using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Linq;

namespace ChaoprayaBoat
{
    public partial class PortListPage : ContentPage
    {
        bool isNavi;
        public Coordinate SelectedPort { get; set; }
        int? routeId;

        List<Coordinate> ports;

        public PortListPage(bool isNavi)
        {
            InitializeComponent();
            this.isNavi = isNavi;

            searchPort.TextChanged += SearchPort_TextChanged;
            portListView.ItemTapped += PortListView_ItemTapped;
            portListView.Refreshing += PortListView_Refreshing;
        }

        public PortListPage(int? routeId) : this(false)
        {
            this.routeId = routeId;
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

            portListView.IsRefreshing = true;

            HttpResponseMessage response;
            if (routeId == null)
            {
                response = await client.GetAsync("api/GetPorts");
            }
            else response = await client.GetAsync($"api/GetPortByRoute?routeId={routeId}");
            portListView.IsRefreshing = false;
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                ports = JArray.Parse(json).ToObject<List<Coordinate>>();
                ports = ports.Select(p => new Coordinate
                {
                    ImageSource = routeId == null ? "emptyflag" : $"flag{routeId}",
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description
                
                                     
                }).ToList();
                portListView.ItemsSource = ports;

            }

        }

        void PortListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var port = e.Item as Coordinate;

            if (isNavi)
            {
                SelectedPort = port;
                Navigation.PopAsync();
            }
            else
            {
                var tp = new TabbedPage();
                tp.Children.Add(new PortPage(port));
                tp.Children.Add(new BoatGoPage(port));
                tp.Children.Add(new BoatBackPage(port));
                var travelPageNav = new PortTravelPage(port);
                tp.Children.Add(travelPageNav);
                tp.Title = port.Name;

                Navigation.PushAsync(tp);
            }
        }

        void PortListView_Refreshing(object sender, EventArgs e)
        {
            GetData();
        }

        void SearchPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length == 0) portListView.ItemsSource = ports;
            else portListView.ItemsSource = ports.Where(p => p.Name.Contains(searchPort.Text)).ToList();
        }
    }
}
