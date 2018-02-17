using System;
using System.ComponentModel.DataAnnotations;

namespace ChaoprayaBoat.Library.Models
{
    public class Cost
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; } //ราคา
        public int PortCount { get; set; } //จำนวนสถานี

        //FK
        public int RouteId { get; set; }

        public Route Route { get; set; }
    }
}
