using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.Coordinates
{
    public class NewModel : PageModel
    {
        ApplicationDbContext db;

        public NewModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Coordinate Coordinate { get; set; }

        public SelectList CoordinateType { get; set; }


        public void OnGet() //กดขึ้นตอนโหลดpageครั้งแรก
        {
            var coordinateType = db.CoordinateTypes
                                .ToList();

            CoordinateType = new SelectList(coordinateType, "Id", "Name");
        }

        public IActionResult OnPost()
        {
            db.Add(Coordinate);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }


    }
}
