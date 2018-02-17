using System;
using Newtonsoft.Json;

namespace ChaoprayaBoat.Library.Models
{
    public class BoatHistory
    {
        public int Id { get; set; }
        public DateTime ArriveDateTime { get; set; } //ถึงจุดตรงนั้นกี่โมง


        //FK
        public int TimeTableId { get; set; }
        public int CoordinateId { get; set; }

        public Coordinate Coordinate { get; set; }
        public TimeTable TimeTable { get; set; }


    }
}
