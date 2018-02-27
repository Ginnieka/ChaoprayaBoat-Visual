using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ChaoprayaBoat.Library.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [Required]
        [MaxLength(45)]
        public string Lastname { get; set; }

        [MaxLength(45)]
        public string Email { get; set; }

        [MaxLength(45)]
        [DataType(DataType.Password)]
        [JsonIgnore]
        public string Password { get; set; }


        public string FacebookToken { get; set; }

        [MaxLength(50)]
        public string FacebookId { get; set; }

        [MaxLength(256)]
        public string FacebookPicture { get; set; }

   
        //[Required]      
        //[JsonIgnore]
        //public double Latitude { get; set; }

        //[Required]   
        //[JsonIgnore]
        //public double Longtitude { get; set; }

        [MaxLength(45)]
        public string Status { get; set; }

        [MaxLength(45)]
        public string Position { get; set; }

        public bool IsDeleted { get; set; }

        [JsonIgnore]
        public List<TimeTable> TimeTables { get; set; }

        //FK
        [Required]
        [Display(Name = "Member Type")]
        public int MemberTypeId { get; set; }

        [JsonIgnore]
        public MemberType MemberType { get; set; }

        [JsonIgnore]
        public List<MemberHistory> MemberHistories { get; set; }

               
        [NotMapped]
        [JsonIgnore]
        public string Fullname
        {
            get
            {
                return Name + " " + Lastname;
            }
        }

    }
}
