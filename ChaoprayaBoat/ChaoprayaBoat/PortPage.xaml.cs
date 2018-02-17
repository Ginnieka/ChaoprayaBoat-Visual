using System;
using System.Collections.Generic;
using ChaoprayaBoat.Library.Models;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class PortPage : ContentPage
    {
        Coordinate port;
        public PortPage()
        {
            InitializeComponent();
            naviButton.Clicked += NaviButton_Clicked;
        }

        public PortPage(Coordinate port) :this()
        {
            this.port = port;

            BindingContext = port;
            //namePortLabel.Text = port.Name;
        }

        void NaviButton_Clicked(object sender, EventArgs e)
        {
            var request = string.Format($"http://maps.google.com/?daddr={port.Latitude},{port.Longtitude}");

            Device.OpenUri(new Uri(request));
        }


    }
}
