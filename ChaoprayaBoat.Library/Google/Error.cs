using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Library.Google
{
    public class Error
    {
        public string error_message { get; set; }
        public IList<object> html_attributions { get; set; }
        public IList<object> results { get; set; }
        public string status { get; set; }
    }
}
