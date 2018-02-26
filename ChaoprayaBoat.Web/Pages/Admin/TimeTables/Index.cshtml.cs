using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Library.Web;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public SelectList MonthList { get; set; }
        public SelectList YearList { get; set; }
        public SelectList DestinationList { get; set; }

        [BindProperty]
        public int Month { get; set; }
        [BindProperty]
        public int Year { get; set; }
        [BindProperty]
        public string Destination { get; set; }

        public void OnPostDelete(int id) // method ลบด้วยid
        {
            var timetable = db.TimeTables.Find(id);

            if (timetable != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                db.Remove(timetable);
                db.SaveChanges();
            }

            OnGet(Month, Year, Destination);
        }

        public void OnGet(int? month, int? year, string dest)
        {
            BindDropDownList();

            if (month == null) Month = DateTime.Today.Month;
            else Month = month.Value;

            if (year == null) Year = DateTime.Today.Year;
            else Year = year.Value;

            if (dest == null) Destination = "ขาไป";
            else Destination = dest;

            TimeTables = db.TimeTables
                           .Include(x => x.Member)
                           .Include(x => x.Boat )
                           .Where(x => x.DateTime.Month == Month && x.DateTime.Year == Year && x.Destination == Destination)
                           .ToList(); //คำสั่งเรียกจาก DB
        }

        void BindDropDownList()
        {
            MonthList = new SelectList(new List<Month> {
                new Month { Id=1, Name="มกราคม"},
                new Month { Id=2, Name="กุมภาพันธ์"},
                new Month { Id=3, Name="มีนาคม"},
                new Month { Id=4, Name="เมษายน"},
                new Month { Id=5, Name="พฤษภาคม"},
                new Month { Id=6, Name="มิถุนายน"},
                new Month { Id=7, Name="กรกฎาคม"},
                new Month { Id=8, Name="สิงหาคม"},
                new Month { Id=9, Name="กันยายน"},
                new Month { Id=10, Name="ตุลาคม"},
                new Month { Id=11, Name="พฤศจิกายน"},
                new Month { Id=12, Name="ธันวาคม"},
            }, "Id", "Name");

            var year = db.TimeTables.OrderBy(t => t.DateTime.Year).Select(t => t.DateTime.Year).Distinct().ToList();
            if (DateTime.Today.Month == 12) year.Add(DateTime.Today.Year + 1);
            YearList = new SelectList(year);

            DestinationList = new SelectList(new List<string> { "ขาไป", "ขากลับ" });

        }

        public void OnPostSearch()
        {
            BindDropDownList();
            TimeTables = db.TimeTables
                           .Include(x => x.Member)
                           .Include(x => x.Boat)
                           .Where(x => x.DateTime.Month == Month && x.DateTime.Year == Year && x.Destination == Destination)
                           .ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
