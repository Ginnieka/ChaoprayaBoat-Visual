using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaoprayaBoat.Library.Models;
using ChaoprayaBoat.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaoprayaBoat.Web.Pages.Admin.Boats
{
    public class EditModel : PageModel
    {
        ApplicationDbContext db;
        public EditModel(ApplicationDbContext db)
        {
            this.db = db;
        }

        [BindProperty]
        public Boat Boat { get; set; }

        public void OnGet(string id)
        {
            Boat = db.Boats.Find(id);
        }

        public IActionResult OnPost()
        {
            db.Update(Boat);
            db.SaveChanges();

            return RedirectToPage("./Index");
        }
    }
}
