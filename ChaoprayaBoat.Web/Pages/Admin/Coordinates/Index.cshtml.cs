using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;

namespace ChaoprayaBoat.Web.Pages.Admin.Coordinates
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Coordinate> Coordinates { get; set; }

        public void OnPostDelete(int id) // method ลบด้วยid
        {
            var coordinate = db.Coordinates.Find(id);

            if (coordinate != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                db.Remove(coordinate);
                db.SaveChanges();
            }

            OnGet();
        }

        public void OnGet()
        {
            Coordinates = db.Coordinates.ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
