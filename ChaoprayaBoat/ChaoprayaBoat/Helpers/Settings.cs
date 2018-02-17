using System;
using Plugin.Settings;

namespace ChaoprayaBoat.Helpers
{
    public static class Settings
    {
        public static string Email
        {
            get{ return CrossSettings.Current.GetValueOrDefault("Email", ""); }
            set{ CrossSettings.Current.AddOrUpdateValue("Email",value);   }
        }

        public static string Fullname
        {
            get { return CrossSettings.Current.GetValueOrDefault("Fullname", ""); }
            set { CrossSettings.Current.AddOrUpdateValue("Fullname", value); }
        }
    }
}
