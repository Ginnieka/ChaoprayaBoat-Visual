using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.Travels
{
    public class NewModel : PageModel
    {
        ApplicationDbContext db;

        public NewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Travel Travel { get; set; }

        public SelectList Coordinates { get; set; }
        public SelectList CoordinateType { get; set; }


        public void OnGet() //กดขึ้นตอนโหลดpageครั้งแรก
        {
            var coordinates = db.Coordinates
                                .Where(x => x.CoordinateTypeId == 2) 
                                //เราเลือก showเฉพาะท่าเรืออย่าง  เดียว
                                .ToList();

            Coordinates = new SelectList(coordinates,"Id","Name");
        }

        public IActionResult OnPost() //โชว์ เมื่อเรากดsubmit
        {
            db.Add(Travel);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }


    }
}
