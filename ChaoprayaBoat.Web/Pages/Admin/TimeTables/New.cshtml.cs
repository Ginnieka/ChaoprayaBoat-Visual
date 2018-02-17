using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.TimeTables
{
    public class NewModel : PageModel
    {
        ApplicationDbContext db;

        public NewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public TimeTable TimeTable { get; set; }

        public SelectList Members { get; set; }
        public SelectList Routes { get; set; }
        public SelectList Boats { get; set; }
        public SelectList Destinations { get; set; }

        public void OnGet() //กดขึ้นตอนโหลดpageครั้งแรก
        {
            var members = db.Members
                            .Where(x => x.MemberTypeId == 2)
                                //เราเลือก showเฉพาะอย่างเดียว
                                .ToList();

            Members = new SelectList(members, "Id", "Name");

            var routes = db.Routes.ToList();

            Routes = new SelectList(routes, "Id", "FlagColor");

            var boats = db.Boats.ToList();

            Boats = new SelectList(boats, "Id","Id");

            var destinations = new List<string>();
            destinations.Add("ขาไป");
            destinations.Add("ขากลับ");
            Destinations = new SelectList(destinations);
            
        }

        public IActionResult OnPost() //โชว์ เมื่อเรากดsubmit
        {
            db.Add(TimeTable);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
