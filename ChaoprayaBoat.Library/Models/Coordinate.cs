using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChaoprayaBoat.Library.Models
{
    public class Coordinate 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
        public int Sequence { get; set; }   //จัดเส้นทางเรือ
        public string CodePort { get; set; }

        // ตารางที่อ้างถึงcoordinate
        public List<Travel> Travels { get; set; }
        public List<RouteCoordinate> RouteCoordinates { get; set; }
        [JsonIgnore]
        public List<BoatHistory> BoatHistories { get; set; }


        //FK
        public int CoordinateTypeId { get; set; } = 2; //เก็ประเภทท่าเรือกับเส้นระหว่างทางเรือ

        public CoordinateType CoordinateType { get; set; }

        public void GetDistacneInfo(double latitude, double longitude)
        {
            Distance = GetDistance(latitude, longitude);
            Duration = TimeSpan.FromMinutes((Distance / 1000) * 1);
        }

        public double GetDistance(double latitude, double longitude)
        {
            var olat = latitude - Latitude;
            var olong = longitude - Longtitude;
            var sqrt = Math.Sqrt(Math.Pow(olat, 2) + Math.Pow(olong, 2));
            return (sqrt / (1.0f / 108.4f)) * 1000;
        }

        [NotMapped]
        [JsonIgnore]
        public string LatLongText
        {
            get
            {
                return string.Format("{0:0.0000}, {1:0.0000}", Latitude, Longtitude);
            }
        }

        [NotMapped]
        [JsonIgnore]
        public double Distance { get; set; }

        [NotMapped]
        [JsonIgnore]
        public TimeSpan Duration { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string DistanceDurationBoatText
        {
            get
            {
                if (Distance > 0)
                {
                    return string.Format("เรืออยู่ห่างจากท่าเรือ {0:0.000} กม. และจะถึงภายใน {1:0} ชม. {2:0} นาที", Distance / 1000, Duration.TotalHours, Duration.Minutes);
                }
                else return "เรือถึงท่าเรือแล้ว";
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string DistancePortText
        {
            get
            {                
                return string.Format("ระยะทางถึงท่าเรือประมาณ {0:0.000} กม.", Distance / 1000);
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string ImagePort1
        {
            get
            {
                return $"port{Id}_1";
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string ImagePort2
        {
            get
            {
                return $"port{Id}_2";
            }
        }
    }
}
