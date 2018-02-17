using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChaoprayaBoat.Library.Models
{
    public class CoordinateType //เส้นทาง
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Coordinate> Coordinates { get; set; }
    }
}
