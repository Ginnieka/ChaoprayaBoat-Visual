using System;
using System.Collections.Generic;
using ChaoprayaBoat.Library.Models;
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
        }

        public PortTravelDetail(Travel travel) : this()
        {
            this.travel = travel;
        }

        void CloseButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
