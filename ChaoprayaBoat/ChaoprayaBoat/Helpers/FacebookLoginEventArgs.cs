using System;
using ChaoprayaBoat.Library.Facebook;

namespace ChaoprayaBoat
{
    public class FacebookLoginEventArgs : EventArgs
    {
        public FacebookUser User { get; set; }
    }
}