using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;

namespace ChaoprayaBoat.Web.Pages.Admin.Boats
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Boat> Boats { get; set; }

        public void OnPostDelete(string id) // method ลบด้วยid
        {
            var boat = db.Boats.Find(id);

            if (boat != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                boat.IsDeleted = true;
                db.SaveChanges();
            }

            OnGet();

        }

        public void OnGet()
        {
            Boats = db.Boats
                      .Where(x => !x.IsDeleted)
                      .ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
