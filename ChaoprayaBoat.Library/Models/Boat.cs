using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChaoprayaBoat.Library.Models
{
    public class Boat
    {
        //Annotation
        //Obj ใน class Boat
        [Key]
        [Required]
        [Display(Name = "License Plate")] 
        public string Id { get ; set; } //ทะเบียนเรือ

        //public double Latitude { get; set; }

        //public double Longtitude { get; set; }

        public bool IsDeleted { get; set; }
        public string Type { get; set; }
        public string Detail { get; set; }

        public List<TimeTable> TimeTables { get; set; }

       
    }
}
