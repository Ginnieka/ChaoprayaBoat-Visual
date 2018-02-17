using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;


namespace ChaoprayaBoat.Web.Pages.Admin.Travels
{
    public class IndexModel : PageModel
    {
        ApplicationDbContext db;

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Travel> Travels { get; set; }


        public void OnPostDelete(int id) // method ลบด้วยid
        {
            var travel = db.Travels.Find(id);

            if (travel != null) //ตรวจสอบทำให้การค้นหาไม่error makesure
            {
                db.Remove(travel);
                db.SaveChanges();
            }

            OnGet();
        }

        public void OnGet()
        {
            Travels = db.Travels.ToList(); //คำสั่งเรียกจาก DB
        }
    }
}
