using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.Coordinates
{
    public class EditModel : PageModel
    {
        ApplicationDbContext db;
        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Coordinate Coordinate { get; set; }

        public SelectList CoordinateType { get; set; }

        public void OnGet(int id)
        {
            Coordinate = db.Coordinates.Find(id);


            CoordinateType = new SelectList(db.CoordinateTypes.ToList(), "Id", "Name");
        }

        public IActionResult OnPost()
        {
            db.Update(Coordinate);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
