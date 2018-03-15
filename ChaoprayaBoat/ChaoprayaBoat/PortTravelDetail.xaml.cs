using System;
using System.Collections.Generic;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    
    public partial class PortTravelDetail : ContentPage
    {
        Travel travel;
        public PortTravelDetail()
        {
            InitializeComponent();
            closeButton.Clicked += CloseButton_Clicked;
            //GetData();

        }

        public PortTravelDetail(Travel travel) : this()
        {
            this.travel = travel;
            BindingContext = travel;
        }

        //async void GetData()
        //{
        //    var client = new System.Net.Http.HttpClient();
        //    client.BaseAddress = App.BaseAddress;

        //    var response = await client.GetAsync($"api/GetTravel");
        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        var json = await response.Content.ReadAsStringAsync();
        //        var travels = JArray.Parse(json).ToObject<List<Travel>>();
        //        //portTravelListView.ItemsSource = travels;

        //    }


        //}


        void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
