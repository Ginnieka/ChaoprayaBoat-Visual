using System;
using Plugin.Settings;

namespace ChaoprayaBoat.Helpers
{
    public static class Settings
    {
        public static bool IsLoggedIn
        {
            get { return CrossSettings.Current.GetValueOrDefault("IsLoggedIn", false); }
            set { CrossSettings.Current.AddOrUpdateValue("IsLoggedIn", value); }
        }

        public static int MemberId
        {
            get { return CrossSettings.Current.GetValueOrDefault("MemberId", 0); }
            set { CrossSettings.Current.AddOrUpdateValue("MemberId", value); }
        }

        public static string Fullname
        {
            get { return CrossSettings.Current.GetValueOrDefault("Fullname", ""); }
            set { CrossSettings.Current.AddOrUpdateValue("Fullname", value); }
        }

        public static string Picture
        {
            get { return CrossSettings.Current.GetValueOrDefault("Picture", ""); }
            set { CrossSettings.Current.AddOrUpdateValue("Picture", value); }
        }

        public static bool IsTutorial
        {
            get { return CrossSettings.Current.GetValueOrDefault("IsTutorial", false); }
            set { CrossSettings.Current.AddOrUpdateValue("IsTutorial", value); }
        }

        public static string[] ReadPermissions = new string[] 
        {
            "public_profile"
        };

        public static string[] PublishPermissions = new string[] 
        {
            "publish_actions"
        };

        public static string AppName { get; } = "เรือด่วนเจ้าพระยา";
    }
}
