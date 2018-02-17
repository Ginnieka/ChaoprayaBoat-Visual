using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Linq;


namespace ChaoprayaBoat.Library.Models
{
    public class TimeTable
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public Boolean IsActive { get; set; } = true; //สถานะเรือที่กำลังวิ่ง
        public string Cashier { get; set; }

        //FK
        [Display(Name = "Boat")]
        public string BoatId { get; set; }
        [Display(Name = "Route")]
        public int RouteId { get; set; }

        public string Destination { get; set; } //ขาไปขากลับของเรือ

        public int MemberId { get; set; }
       

        [JsonIgnore]
        public Boat Boat { get; set; }

        public Route Route { get;  set; }

        public Member Member { get; set; }

        [JsonIgnore]
        public List<BoatHistory> BoatHistories { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string DateText
        {
            get
            {
                return DateTime.ToString("HH:mm");
            }
        }

        [JsonIgnore]
        [NotMapped]
        public string Detail
        {
            get
            {
                if (Route != null)
                {
                    return $"{BoatId}, {Route.FlagColor}, {Cashier},{Destination}";
                }
                else return $"{BoatId}, {Cashier}";
            }
        }

      

    }
}
