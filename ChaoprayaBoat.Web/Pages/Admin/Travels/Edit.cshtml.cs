using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaoprayaBoat.Web.Pages.Admin.Travels
{
    public class EditModel : PageModel
    {
        ApplicationDbContext db;
        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Travel Travel { get; set; }

        public SelectList Coordinates { get; set; }

        public void OnGet(int id)
        {
            Travel = db.Travels.Find(id);

            var coordinates = db.Coordinates
                                .Where(x => x.CoordinateTypeId == 1)
                                //เราเลือก showเฉพาะท่าเรืออย่าง  เดียว
                                .ToList();

            Coordinates = new SelectList(coordinates, "Id", "Name");
        }

        public IActionResult OnPost()
        {
            db.Update(Travel);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
