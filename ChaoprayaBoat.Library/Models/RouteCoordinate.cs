using System;
using System.ComponentModel.DataAnnotations;

namespace ChaoprayaBoat.Library.Models
{
    public class RouteCoordinate //กำหนดเส้นทางเดินเรือ
    {
        [Key]
        public int Id { get; set; }

        //FK
        public int RouteId { get; set; }
        public int CoordinateId { get; set; }

        public Route Route { get; set; }
        public Coordinate Coordinate { get; set; }

    }
}
