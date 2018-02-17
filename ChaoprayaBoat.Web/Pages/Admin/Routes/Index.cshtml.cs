using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;

namespace ChaoprayaBoat.Web.Pages.Admin.Routes
{
  
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Route> Routes { get; set; }

        public void OnPostDelete(int id) // method ลบด้วยid
        {
            var route = db.Routes.Find(id);

            if (route != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                db.Remove(route);
                db.SaveChanges();
            }

            OnGet();
        }

        public void OnGet()
        {
            Routes = db.Routes.ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
