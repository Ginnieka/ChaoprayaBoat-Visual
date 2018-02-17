using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChaoprayaBoat.Web.Pages.Admin.TimeTables
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<TimeTable> TimeTables { get; set; }


        public void OnPostDelete(int id) // method ลบด้วยid
        {
            var timetable = db.TimeTables.Find(id);

            if (timetable != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                db.Remove(timetable);
                db.SaveChanges();
            }

            OnGet();
        }

        public void OnGet()
        {
            TimeTables = db.TimeTables
                           .Include(x => x.Member)
                           .Include(x => x.Boat )
                           .ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
