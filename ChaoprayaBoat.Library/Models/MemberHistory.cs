using System;
using System.Collections.Generic;

namespace ChaoprayaBoat.Library.Models
{
    public class MemberHistory
    {
        public int Id { get; set; }
        public DateTime Datetime { get; set; }
        public double SourceCoordinateId { get; set;  }
        public double DestinationCoordinteId { get; set; }

        //FK
        public int MemberId { get; set; }
       
    }
}
