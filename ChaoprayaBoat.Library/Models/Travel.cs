﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChaoprayaBoat.Library.Models
{
    public class Travel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set;  }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longtitude { get; set; }

        [Required]
        public string Status { get; set; }

        //FK
        [Display(Name = "Port")]
        [Required]
        public int CoordinateId { get; set; }

        public Coordinate Coordinate { get; set; }


        [NotMapped]
        [JsonIgnore]
        public string ImageTravel3
        {
            get
            {
                return $"travel{Id}_1";
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string ImageSource { get; set; }
       
    }
}
