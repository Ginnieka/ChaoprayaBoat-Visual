using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChaoprayaBoat.Library.Models
{
    public class Route
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Flag Color")] //เปลี่ยนคอลัม มีผลต่อหน้าweb
        public string FlagColor { get; set; } 

        [Display(Name = "Flag Code")] //เปลี่ยนคอลัม มีผลต่อหน้าweb
        public string ColorCode { get; set; }

        public string PriceDesc { get; set; }

        [JsonIgnore]
        public List<TimeTable> TimeTables { get; set; }
        [JsonIgnore]
        public List<RouteCoordinate> RouteCoordinates { get; set; }
        [JsonIgnore]
        public List<Cost> Costs { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string ImageSource
        {
            get
            {
                return $"flag{Id}";
            }
        }

        [NotMapped]
        public double DurationMinute { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string DurationText
        { 
            get
            {
                var duration = TimeSpan.FromMinutes(DurationMinute);

                return string.Format("ระยะเวลาเดินทาง {0:0} นาที", duration.TotalMinutes);
            } 
        }
    }
}
