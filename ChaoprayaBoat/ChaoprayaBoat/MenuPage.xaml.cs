using System;
using System.Collections.Generic;
using ChaoprayaBoat.Library.Models;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace ChaoprayaBoat
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            //logoutButton.Clicked += LogoutButton_Clicked;
            //inforButton.Clicked += MenuButton_Clicked;
            //homeButton.Clicked += MenuButton_Clicked; 

            var myMenus = new List<MyMenu>{
                new MyMenu { Id=1, Text="หน้าแรก", Detail="", ImageSource="home" },
                new MyMenu { Id=3, Text="รายละเอียดการเดินเรือ", Detail="", ImageSource="time" },
                new MyMenu { Id=4, Text="ข้อมูลท่าเรือ", Detail="", ImageSource="list" },
                new MyMenu { Id=5, Text="โทรฉุกเฉิน", Detail="", ImageSource="phone" },
                new MyMenu { Id=6, Text="ออกจากระบบ", Detail="", ImageSource="logout" }
            };

            menuListView.ItemsSource = myMenus;

            menuListView.ItemTapped += MenuListView_ItemTapped;
        }

        async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            var isOK = await DisplayAlert("Logout", "Do you want to logout?", "OK", "Cancel");

            if (isOK)
            {
                Helpers.Settings.Email = "";
                Helpers.Settings.Fullname = "";
            }

            var mp = Parent as MasterDetailPage;
            var app = mp.Parent as App;
            app.MainPage = new LoginPage();
        }

        async void MenuListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var menu = e.Item as MyMenu;
            var mp = Parent as MasterDetailPage;

            switch (menu.Id)
            {
                case 1:
                    mp.Detail = new NavigationPage(new MainPage());
                    break;
                case 3:

                    var client = new System.Net.Http.HttpClient();
                    client.BaseAddress = App.BaseAddress;

                    var response = await client.GetAsync("api/GetRoute");
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var routes = JArray.Parse(json).ToObject<List<Route>>();

                        var tp = new TabbedPage();
                        tp.Title = "รายละเอียดการเดินเรือ";
                        foreach (var route in routes)
                        {
                            var portListPage = new PortListPage(route.Id);
                            portListPage.Title = route.FlagColor;
                            portListPage.Icon = "flag1";
                            tp.Children.Add(portListPage);
                        }
                        mp.Detail = new NavigationPage(tp);
                    }



                    break;
                case 4:
                    mp.Detail = new NavigationPage(new PortListPage(false));
                    break;
                case 5:
                    mp.Detail = new NavigationPage(new EmergencyPage());
                    break;
                case 6:
                    var isOk = await DisplayAlert("ออกจากระบบ", "คุณต้องการออกจากระบบ ?", "ใช่", "ยกเลิก");
                    if (isOk)
                    {
                        var app = mp.Parent as App;
                        app.MainPage = new LoginPage();
                    }
                    else menuListView.SelectedItem = null;
                    break;
                default:
                    break;
            }

            mp.IsPresented = false;

        }

        //void MenuButton_Clicked(object sender, EventArgs e)
        //{
        //    var mp = Parent as MasterDetailPage;

        //    if (sender == inforButton)
        //    {
        //        mp.Detail = new NavigationPage(new PortListPage());
        //    }
        //    else if (sender == homeButton)
        //    {
        //        mp.Detail = new NavigationPage(new MainPage());
        //    }

        //    mp.IsPresented = false;
        //}
    }

    class MyMenu
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Detail { get; set; }
        public string ImageSource { get; set; }
    }
}
