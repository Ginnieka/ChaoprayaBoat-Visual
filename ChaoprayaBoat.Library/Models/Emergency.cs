using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChaoprayaBoat.Library.Models
{
    public class Emergency
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [MaxLength(45)]
        public string Telephone { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string ImageSource
        {
            get
            {
                return $"emer{Id}";
            }
        }
    }
}
