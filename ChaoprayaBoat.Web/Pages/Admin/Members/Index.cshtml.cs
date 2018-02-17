using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaoprayaBoat.Web.Pages.Admin.Members
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Member> Members { get; set; }


        public void OnPostDelete(int id) // method ลบด้วยid
        {
            var member = db.Members.Find(id);

            if (member != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                member.IsDeleted = true;
                db.SaveChanges();
            }

            OnGet();
        }

        public void OnGet()
        {
            Members = db.Members
                        .Where(x => !x.IsDeleted)
                        .ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
