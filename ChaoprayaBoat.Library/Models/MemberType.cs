using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChaoprayaBoat.Library.Models
{
    public class MemberType
    {
        [Key]
        [Required]
        public int Id { get ; set; }
        public string Name { get; set; }


        // ตารางที่อ้างถึงMemberType
        public List<Member> Members { get; set; }

    }
}
