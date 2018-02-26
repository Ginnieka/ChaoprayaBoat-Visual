using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Library.Models
{
    public class MemberHistory
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public double Latitude { get; set;  }
        public double Longitude { get; set; }

        //FK
        public int MemberId { get; set; }
       
    }
}
