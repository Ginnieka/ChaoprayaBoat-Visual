using System;
using System.Collections.Generic;
using System.Net.Http;
using ChaoprayaBoat.Library.Models;
using Plugin.Geolocator;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace ChaoprayaBoatTracker
{
    public partial class KeepCoordinatePage : ContentPage
    {
        List<TimeTable> timeTables;

        public KeepCoordinatePage()
        {
            InitializeComponent();

            logoutButton.Clicked += LogoutButton_Clicked;
            timetableListViewq.ItemSelected += TimetableListViewq_ItemSelected;
        }

        async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            var isOK =  await DisplayAlert("Logout", "Do you want to logout", "OK", "Cancel");

            if (isOK) await Navigation.PopModalAsync();
        }

        public KeepCoordinatePage(List<TimeTable> timetables) :this()
        {
            

            this.timeTables = timetables;
            timetableListViewq.ItemsSource = timetables.Where(x => x.IsActive).ToList();

            //start keep coor
            Device.StartTimer(TimeSpan.FromSeconds(30), OnTime);
            OnTime();
        }

        bool OnTime()
        {
            var currentTimeTables = timeTables.Where(x => x.IsActive)
                                              .OrderBy(x => x.DateTime)
                                              .ToList();

            if (currentTimeTables.Count() > 0)
            {
                var currentTimeTable = currentTimeTables[0];

                driverNameLabel.Text = $"คนขับเรือ : {currentTimeTable.Member.Name}";
                cashierNameLabel.Text = $"พนักงานเก็บค่าโดยสาร : {currentTimeTable.Cashier}";
                plateLabel.Text = $"ทะเบียนเรือ : {currentTimeTable.BoatId}";
                startTimeLabel.Text = string.Format("เริ่มเวลา : {0:HH:mm}",currentTimeTable.DateTime);

                if (DateTime.Now >= currentTimeTable.DateTime)
                {
                    SendLocationToServer(currentTimeTable);
                }
            }

            return true;
        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        async void SendLocationToServer(TimeTable currentTimeTable)
        {
            try
            {
                if (IsLocationAvailable())
                {
                    var position = await CrossGeolocator.Current.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));
                    Debug.WriteLine(string.Format("Location: {0:0.0000}, {1:0.0000}", position.Latitude, position.Longitude), "Chaopraya Boat");

                    var param = new Dictionary<string, string>();
                    param.Add("latitude", position.Latitude.ToString());
                    param.Add("longitude", position.Longitude.ToString());
                    param.Add("timetableid", currentTimeTable.Id.ToString());

                    var content = new System.Net.Http.FormUrlEncodedContent(param);

                    var client = new HttpClient();
                    client.BaseAddress = App.BaseAddress;
                    var response = await client.PostAsync("api/AddBoatLocation", content);
                    //
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        var isActive = bool.Parse(result);
                        currentTimeTable.IsActive = isActive;

                        if (!isActive)
                        {
                            timetableListViewq.ItemsSource = timeTables.Where(x => x.IsActive).ToList();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }

            //await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true, new Plugin.Geolocator.Abstractions.ListenerSettings
            //{
            //    ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
            //    AllowBackgroundUpdates = true,
            //    DeferLocationUpdates = true,
            //    DeferralDistanceMeters = 1,
            //    DeferralTime = TimeSpan.FromSeconds(1),
            //    ListenForSignificantChanges = true,
            //    PauseLocationUpdatesAutomatically = false
            //});

            //CrossGeolocator.Current.PositionChanged += Current_PositionChanged;

        }

        void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => {

                //locationLabel.Text = string.Format("{0}, {1}", e.Position.Latitude, e.Position.Longitude);

            });
        }

        void TimetableListViewq_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            timetableListViewq.SelectedItem = null;
        }
    }
}
